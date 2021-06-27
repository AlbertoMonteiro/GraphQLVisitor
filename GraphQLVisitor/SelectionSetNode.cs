using System;
using System.Collections.Generic;
using System.Linq;
using static Constants;

public class SelectionSetNode : IQueryNode
{
    public SelectionSetNode(string text) => Text = text;

    public string Text { get; }

    public HashSet<IQueryNode> Children { get; } = new();

    public string ToString(string identation) => $"{BOLD_BLUE}{identation}{Text} {{{Environment.NewLine}{string.Join("," + Environment.NewLine, Children.Select(x => x.ToString(identation + "    ")))}{Environment.NewLine}{identation}}}{RESET}";

    public override string ToString() => ToString("");

    public override bool Equals(object obj)
        => obj is SelectionSetNode node && node.Text == Text;

    public override int GetHashCode() => Text.GetHashCode() & "SelectionSetNode".GetHashCode();
}
