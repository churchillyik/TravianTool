using System;
using System.Collections;
using System.Text;
using System.ComponentModel;

namespace Yaowi.Common.Collections
{
  /// <summary>
  /// Sort order enumeration.
  /// </summary>
  /// <remarks>
  /// There is an enumeration with the same name in the System.Windows.Forms namespace!
  /// <br>Since the use of a SortOrder enumeration should not force the import of that
  /// namespace, this enumeration became necessary. Like Microsoft it is my opinion that
  /// "SortOrder" describes its meaning.
  /// </remarks>
  public enum SortOrder
  {
    [Description("Ascending")]
    Ascending,
    [Description("Descending")]
    Descending,
    [Description("None")]
    None
  }

  /// <summary>
  /// Interface extending the IComparer interface by the property SortOrder.
  /// </summary>
  public interface ISortComparer : IComparer
  {
    /// <summary>
    /// Gets or sets the SortOrder.
    /// </summary>
    SortOrder SortOrder { get; set; }
  }

  #region SortComparerBase

  /// <summary>
  /// Abstract class providing the common ISortComparer funtionalities.
  /// </summary>
  public abstract class SortComparerBase : ISortComparer
  {
    protected SortOrder sortorder = SortOrder.Ascending;

    /// <summary>
    /// <see cref="ISortComparer"/>
    /// </summary>
    public SortOrder SortOrder
    {
      get { return sortorder; }
      set { sortorder = value; }
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public SortComparerBase()
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="sortOrder"></param>
    public SortComparerBase(SortOrder sortOrder)
    {
      this.sortorder = sortOrder;
    }

    /// <summary>
    /// Returns whether one of two to-be compared Objects is null or equals an empty String.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    protected bool HasEmptyValue(object x, object y)
    {
      if (x == null || x.ToString().Equals("") || y == null || y.ToString().Equals(""))
        return true;

      return false;
    }

    /// <summary>
    /// Compares Objects of which at least one is null or empty.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    protected int CompareEmpty(object x, object y)
    {
      if ((x == null || x.ToString().Equals("")) && (y == null || y.ToString().Equals("")))
      {
        // Both are null or empty
        return 0;
      }
      else if (x == null || x.ToString().Equals(""))
      {
        // x is null or empty
        if (this.sortorder == SortOrder.Ascending)
          return String.Compare("", (string)y);
        else
          return String.Compare((string)y, "");
      }
      else if (y == null || y.ToString().Equals(""))
      {
        // y null or empty
        if (this.sortorder == SortOrder.Ascending)
          return String.Compare((string)x, "");
        else
          return String.Compare("", (string)x);
      }

      return 0;
    }

    /// <summary>
    /// Compare. Has to be implemented by concrete implementations.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public abstract int Compare(object x, object y);
  }

  #endregion SortComparerBase

  #region ISortComparer Implementations

  /// <summary>
  /// Compares strings.
  /// </summary>
  public class StringComparer : SortComparerBase
  {
    /// <summary>
    /// Compare.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public override int Compare(object x, object y)
    {
      if (sortorder == SortOrder.None)
        return 0;

      if(HasEmptyValue(x, y))
        return CompareEmpty(x, y);

      if(this.sortorder== SortOrder.Ascending)
        return String.Compare((string)x, (string)y);
      else
        return String.Compare((string)y, (string)x);
    }
  }

  /// <summary>
  /// Compares dates (even when delivered as strings).
  /// </summary>
  public class DateComparer : SortComparerBase
  {
    /// <summary>
    /// Compare.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public override int Compare(object x, object y)
    {
      // If there's nothing to sort everything is equal
      if (base.sortorder == SortOrder.None)
        return 0;

      // Handle the case one of the values is null or empty
      if (base.HasEmptyValue(x, y))
        return base.CompareEmpty(x, y);

      // Set defaults
      DateTime x1 = DateTime.MinValue;
      DateTime y1 = DateTime.MinValue;

      if (DateTime.TryParse(x.ToString(), out x1) && DateTime.TryParse(y.ToString(), out y1))
      {
        if (base.sortorder == SortOrder.Ascending)
          return DateTime.Compare(x1, y1);
        else
          return DateTime.Compare(y1, x1);
      }
      else
      {
        return 0;
      }
    }
  }

  /// <summary>
  /// Compares numeric values (even when delivered as strings).
  /// </summary>
  public class NumericComparer : SortComparerBase
  {
    /// <summary>
    /// Compare.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public override int Compare(object x, object y)
    {
      if (sortorder == SortOrder.None)
        return 0;

      if (HasEmptyValue(x, y))
        return CompareEmpty(x, y);

      Decimal x1 = 0;
      Decimal y1 = 0;

      if (Decimal.TryParse(x.ToString(), out x1) && Decimal.TryParse(y.ToString(), out y1))
      {
        if (this.sortorder == SortOrder.Ascending)
          return Decimal.Compare(x1, y1);
        else
          return Decimal.Compare(y1, x1);
      }
      else
      {
        return 0;
      }
    }
  }

  /// <summary>
  /// Compares combinations of German postal codes and city names by the city name.
  /// <br>E.g. in the expression "22307 Hamburg" "Hamburg" will be the comparison criteria.
  /// </summary>
  public class GermanPostalCodeCityByCityComparer : SortComparerBase
  {
    /// <summary>
    /// Compare.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public override int Compare(object x, object y)
    {
      if (sortorder == SortOrder.None)
        return 0;

      if (HasEmptyValue(x, y))
        return CompareEmpty(x, y);

      String x1 = x.ToString();
      String y1 = y.ToString();

      x1 = x1.Substring(6);
      y1 = y1.Substring(6);

      if (this.sortorder == SortOrder.Ascending)
        return String.Compare((string)x1, (string)y1);
      else
        return String.Compare((string)y1, (string)x1);
    }
  }

  /// <summary>
  /// Compares combinations of German postal codes and city names by the postal code.
  /// <br>E.g. in the expression "22307 Hamburg" "22307" will be the comparison criteria.
  /// </summary>
  public class GermanPostalCodeCityByPostalCodeComparer : NumericComparer
  {
    /// <summary>
    /// Compare.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public override int Compare(object x, object y)
    {
      if (sortorder == SortOrder.None)
        return 0;

      if (HasEmptyValue(x, y))
        return CompareEmpty(x, y);

      String x1 = x.ToString();
      String y1 = y.ToString();
      
      x1 = x1.Split('|')[0];
      y1 = y1.Split('|')[0];

//      x1 = x1.Substring(0, 5);
//      y1 = y1.Substring(0, 5);

      return base.Compare(x1, y1);
    }
  }

  #endregion ISortComparer Implementations
}
