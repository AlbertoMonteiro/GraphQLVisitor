using System;
using System.Linq.Expressions;
using static Constants;

var visitor = new GraphQLVisitor();

Expression<Func<Person, object>> projection = p => new
{
    p.Identity,
    p.Name,
    p.BirthDate,
    p.AuditData.CreatedOn,
    NomeProduto = p.FavotireProducts[0].Name,
    p.FavotireProducts[0].Price.Value,
    p.FavotireProducts[0].Price.Currency,
    DataCriacaoProduto = p.FavotireProducts[0].AuditData.CreatedOn,
    p.FavotireProducts[0].AuditData.LastUpdatedOn,
};

visitor.Visit(projection);

Console.WriteLine(BOLD_GREEN + "### VERSION 01" + RESET);
Console.WriteLine(visitor.GraphQLQuery);

visitor = new GraphQLVisitor();

projection = p => new
{
    p.Identity,
    p.Name,
    p.BirthDate,
    p.AuditData.CreatedOn,
    FavotireProducts = new
    {
        p.FavotireProducts[0].Name,
        p.FavotireProducts[0].Price.Value,
        p.FavotireProducts[0].Price.Currency,
        p.FavotireProducts[0].AuditData.CreatedOn,
        p.FavotireProducts[0].AuditData.LastUpdatedOn,
    }
};

visitor.Visit(projection);

Console.WriteLine(BOLD_GREEN + "### VERSION 02" + RESET);
Console.WriteLine(visitor.GraphQLQuery);

visitor = new GraphQLVisitor();

projection = p => new
{
    p.Identity,
    p.Name,
    p.BirthDate,
    AuditData = new
    {
        p.AuditData.CreatedOn,
    },
    FavotireProducts = new
    {
        p.FavotireProducts[0].Name,
        Price = new
        {
            p.FavotireProducts[0].Price.Value,
            p.FavotireProducts[0].Price.Currency,
        },
        AuditData = new
        {
            p.FavotireProducts[0].AuditData.CreatedOn,
            p.FavotireProducts[0].AuditData.LastUpdatedOn,
        }
    }
};

visitor.Visit(projection);

Console.WriteLine(BOLD_GREEN + "### VERSION 03" + RESET);
Console.WriteLine(visitor.GraphQLQuery);