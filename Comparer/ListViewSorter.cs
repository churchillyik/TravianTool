using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Yaowi.Common.Collections;

namespace Yaowi.Common.Windows.Controls
{
  /// <summary>
  /// Sorts a ListView by arbitray columns.
  /// </summary>
  public class ListViewSorter : IDisposable
  {
    #region Members

    private Dictionary<string, ISortComparer> comparercollection = new Dictionary<string, ISortComparer>();
    private int lastsortcolumn = -1;
    private Yaowi.Common.Collections.SortOrder sortorder = Yaowi.Common.Collections.SortOrder.Descending;
    private ListView listview = null;
    private string imagekeyup = "up";  // Default, but mostly not correct
    private string imagekeydown = "down"; // Default, but mostly not correct

    #endregion Members

    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    public ListViewSorter()
    {
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="listView"></param>
    public ListViewSorter(ListView listView)
    {
      this.ListView = listView;
    }

    #endregion Constructors

    #region Properties

    /// <summary>
    /// Gets or sets the ListView.
    /// </summary>
    public ListView ListView
    {
      get { return listview; }
      set
      {
        // Unregister previous EventHandler
        if (this.listview != null)
          this.listview.ColumnClick -= this.listview_ColumnClick;

        listview = value;

        // Register EventHandler
        if(listview!=null)
          this.listview.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listview_ColumnClick);
      }
    }

    /// <summary>
    /// Gets or sets the Collection of ISortComparers.
    /// </summary>
    public Dictionary<string, ISortComparer> ColumnComparerCollection
    {
      get { return comparercollection; }
      set { comparercollection = value; }
    }

    /// <summary>
    /// Key of the arrow-up image in the ListViews SmallImageList.
    /// </summary>
    public string ImageKeyUp
    {
      get { return imagekeyup; }
      set { imagekeyup = value; }
    }

    /// <summary>
    /// Key of the arrow-down image in the ListViews SmallImageList.
    /// </summary>
    public string ImageKeyDown
    {
      get { return imagekeydown; }
      set { imagekeydown = value; }
    }

    #endregion Properties

    #region Methods

    /// <summary>
    /// Handles the ListViews ColumnClick event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void listview_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      Sort(e.Column);
    }

    /// <summary>
    /// Sorts the ListView by the specified column.
    /// </summary>
    /// <param name="column"></param>
    protected void Sort(int column)
    {
      // Toggle ASC and DESC
      if (column == lastsortcolumn)
      {
        if (sortorder == Yaowi.Common.Collections.SortOrder.Ascending)
          sortorder = Yaowi.Common.Collections.SortOrder.Descending;
        else
          sortorder = Yaowi.Common.Collections.SortOrder.Ascending;
      }
      else
      {
        sortorder = Yaowi.Common.Collections.SortOrder.Ascending;
      }

      lastsortcolumn = column;

      // Get the columns comparer (if the column ist registered use the StringComparer by default)
      ISortComparer c = null;
      if (comparercollection.ContainsKey(this.ListView.Columns[column].Text))
        c = comparercollection[this.ListView.Columns[column].Text];
      else
        c = new Yaowi.Common.Collections.StringComparer();

      // Initialize the ListViewItemComparer
      ListViewItemComparer lvc = new ListViewItemComparer(column, c);
      lvc.SortOrder = sortorder;
      this.ListView.ListViewItemSorter = lvc;

      // Sort!
      this.ListView.Sort();

      // Set ColumnHeaders image
      SetImage(column, sortorder);
    }

    /// <summary>
    /// Sets the image of the sorted ColumnHeader.
    /// </summary>
    /// <param name="column"></param>
    /// <param name="sortorder"></param>
    protected void SetImage(int column, Yaowi.Common.Collections.SortOrder sortorder)
    {
      // Nothing to do
      if (this.listview.SmallImageList == null)
        return;

      string key = String.Empty;

      foreach (ColumnHeader ch in this.ListView.Columns)
      {
        ch.ImageKey = null;
      }

      if (sortorder == Yaowi.Common.Collections.SortOrder.Ascending)
        key = imagekeyup;
      else
        key = imagekeydown;

      if (key!=null && this.listview.SmallImageList.Images.ContainsKey(key))
      {
        this.ListView.Columns[column].ImageKey = key;
      }
    }

    /// <summary>
    /// Clean up.
    /// </summary>
    public void Dispose()
    {
      // Make sure there is no registered EventHandler left
      if (this.listview != null)
        this.listview.ColumnClick -= this.listview_ColumnClick;

      comparercollection.Clear();
    }

    #endregion Methods
  }
}
