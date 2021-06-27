using System.Collections.Generic;
using System.Linq.Expressions;

public class GraphQLVisitor : ExpressionVisitor
{
    private readonly Stack<SelectionSetNode> _nodeHierarchy = new();
    private readonly SelectionSetNode _root = new("Pessoas");
    private ParameterExpression _parameter;

    public string GraphQLQuery => _root.ToString().Trim();

    protected override Expression VisitLambda<T>(Expression<T> node)
    {
        _parameter = node.Parameters[0];
        return base.VisitLambda(node);
    }

    protected override Expression VisitMember(MemberExpression node)
    {
        var isSimpleValue = node.Type.IsValueType || node.Type == typeof(string);
        IQueryNode queryNode = isSimpleValue ? new FieldNode(node.Member.Name) : new SelectionSetNode(node.Member.Name);

        var parentQueryNode = _root;
        if (node.Expression != _parameter)
        {
            _ = Visit(node.Expression);
            parentQueryNode = _nodeHierarchy.Pop();
        }

        if (parentQueryNode.Children.TryGetValue(queryNode, out var actualNode) && actualNode is SelectionSetNode newNode)
            queryNode = newNode;
        else
            parentQueryNode.Children.Add(queryNode);

        if (queryNode is SelectionSetNode selectionSetNode)
            _nodeHierarchy.Push(selectionSetNode);

        return node;
    }

    protected override Expression VisitBinary(BinaryExpression node)
        => node.NodeType == ExpressionType.ArrayIndex ? base.Visit(node.Left) : base.VisitBinary(node);
}
