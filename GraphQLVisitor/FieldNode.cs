using static Constants;

public class FieldNode : IQueryNode
{
    public FieldNode(string text) => Text = text;

    public string Text { get; }

    public string ToString(string identation) => BOLD_RED + identation + Text + RESET;

    public override bool Equals(object obj)
        => obj is FieldNode leaf && leaf.Text == Text;

    public override int GetHashCode() => Text.GetHashCode();
}
