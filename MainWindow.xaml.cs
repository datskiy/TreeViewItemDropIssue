using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;
using Windows.ApplicationModel.DataTransfer;

namespace TreeViewItemDropIssue;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    public TreeViewTestGroupItem[] GroupsItemSource { get; } =
    [
        new TreeViewTestGroupItem("Group #1"),
        new TreeViewTestGroupItem("Group #2")
    ];

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Grid_DragStarting(UIElement sender, DragStartingEventArgs args)
    {
        Debug.WriteLine("[-^-] Grid_DragStarting");

        args.Data.RequestedOperation = DataPackageOperation.Copy;
        args.Data.SetData("TestKey", "TestValue");
    }

    private void TreeViewItem_DragOver(object sender, DragEventArgs e)
    {
        Debug.WriteLine("[-->] TreeViewItem_DragOver");

        e.AcceptedOperation = DataPackageOperation.Copy;
    }

    private void TreeViewItem_DragLeave(object sender, DragEventArgs e)
    {
        Debug.WriteLine("[<--] TreeViewItem_DragLeave");
    }

    private void TreeViewItem_Drop(object sender, DragEventArgs e)
    {
        Debug.WriteLine("[-v-] TreeViewItem_Drop <!>");
    }
}

public sealed class TreeViewTestGroupItem(string title)
{
    public string Title { get; } = title;

    public TreeViewTestMemberItem[] MembersItemSource { get; } =
    [
        new("Member #1"),
        new("Member #2"),
        new("Member #3")
    ];
}

public sealed class TreeViewTestMemberItem(string name)
{
    public string Name { get; } = name;
}

public sealed class TreeViewTemplateSelector : DataTemplateSelector
{
    public DataTemplate? GroupTemplate { get; set; }

    public DataTemplate? MemberTemplate { get; set; }

    protected override DataTemplate? SelectTemplateCore(object item)
    {
        return item switch
        {
            TreeViewTestGroupItem => GroupTemplate,
            TreeViewTestMemberItem => MemberTemplate,
            _ => throw new ArgumentOutOfRangeException(nameof(item))
        };
    }
}

