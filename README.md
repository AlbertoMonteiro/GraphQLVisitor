# GraphQLVisitor

This is an playground where I worked with ExpressionVisitor to create an simple GraphQL query from an given lambda expression

### A lambda expression that returns a flat anonymous object
Since the compiler can infer the name of the anonymous object property from the source object property, I had to given manual names to 2 properties
```csharp
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
```
**Result**
```graphql
Pessoas {
    Identity,
    Name,
    BirthDate,
    AuditData {
        CreatedOn
    },
    FavotireProducts {
        Name,
        Price {
            Value,
            Currency
        },
        AuditData {
            CreatedOn,
            LastUpdatedOn
        }
    }
}
```

### A lambda expression that returns a "semi-flat" anonymous object
In previous example, total flat, I had to manually enter the name of 2 properties, with that way I did just for one(`FavotireProducts`) and for that one I had not to care about name conflits
```csharp
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
```
**Result**
```graphql
Pessoas {
    Identity,
    Name,
    BirthDate,
    InformacoesRegistro {
        CreatedOn
    },
    FavotireProducts {
        Name,
        Price {
            Value,
            Currency
        },
        AuditData {
            CreatedOn,
            LastUpdatedOn
        }
    }
}
```

### A lambda expression that returns a "non-flat" anonymous object
This one, I had to write more, but the object stucture seems like the real graphql query
```csharp
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
```
**Result**
```graphql
Pessoas {
    Identity,
    Name,
    BirthDate,
    InformacoesRegistro {
        CreatedOn
    },
    FavotireProducts {
        Name,
        Price {
            Value,
            Currency
        },
        AuditData {
            CreatedOn,
            LastUpdatedOn
        }
    }
}
```
