#region Copyright (C) 2005-2011 Team MediaPortal

// Copyright (C) 2005-2011 Team MediaPortal
// http://www.team-mediaportal.com
// 
// MediaPortal is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// MediaPortal is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with MediaPortal. If not, see <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Collections.Generic;
using Gentle.Framework;
using TvLibrary.Log;

namespace TvDatabase
{
  /// <summary>
  /// Instances of this class represent the properties and methods of a row in the table <b>Timespan</b>.
  /// Database used by PersonalTVGuide plugin
  /// </summary>
  [TableName("Timespan")]
  public class Timespan : Persistent
  {
    #region Members

    private bool isChanged;
    [TableColumn("idTimespan", NotNull = true), PrimaryKey(AutoGenerated = true)] private int idTimespan;
    [TableColumn("idKeyword", NotNull = true), ForeignKey("Keyword", "idKeyword")] private int idKeyword;
    [TableColumn("startTime", NotNull = true)] private DateTime startTime;
    [TableColumn("endTime", NotNull = true)] private DateTime endTime;
    [TableColumn("dayOfWeek", NotNull = true)] private DayOfWeek dayOfWeek;

    #endregion

    #region Constructors

    /// <summary> 
    /// Create an object from an existing row of data. This will be used by Gentle to 
    /// construct objects from retrieved rows.
    /// </summary> 
    public Timespan(int idTimespan, int idKeyword, DateTime startTime, DateTime endTime, DayOfWeek dayOfWeek)
    {
      this.idTimespan = idTimespan;
      this.idKeyword = idKeyword;
      this.startTime = startTime;
      this.endTime = endTime;
      this.dayOfWeek = dayOfWeek;
    }

    /// <summary> 
    /// Create a new object by specifying all fields (except the auto-generated primary key field). 
    /// </summary> 
    public Timespan(int idKeyword, DateTime startTime, DateTime endTime, DayOfWeek dayOfWeek)
    {
      isChanged = true;
      this.idKeyword = idKeyword;
      this.startTime = startTime;
      this.endTime = endTime;
      this.dayOfWeek = dayOfWeek;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Indicates whether the entity is changed and requires saving or not.
    /// </summary>
    public bool IsChanged
    {
      get { return isChanged; }
    }

    /// <summary>
    /// Property relating to database column idTimespan
    /// </summary>
    public int IdTimespan
    {
      get { return idTimespan; }
    }

    /// <summary>
    /// Property relating to database column startTime
    /// </summary>
    public DateTime StartTime
    {
      get { return startTime; }
      set
      {
        isChanged |= startTime != value;
        startTime = value;
      }
    }

    /// <summary>
    /// Property relating to database column endTime
    /// </summary>
    public DateTime EndTime
    {
      get { return endTime; }
      set
      {
        isChanged |= endTime != value;
        endTime = value;
      }
    }

    /// <summary>
    /// Property relating to database column dayOfWeek
    /// </summary>
    public DayOfWeek Day
    {
      get { return dayOfWeek; }
      set
      {
        isChanged |= dayOfWeek != value;
        dayOfWeek = value;
      }
    }

    public int IdKeyword
    {
      get { return idKeyword; }
    }

    #endregion

    #region Storage and Retrieval

    /// <summary>
    /// Static method to retrieve all instances that are stored in the database in one call
    /// </summary>
    public static IList<TimeSpan> ListAll()
    {
      return Broker.RetrieveList<TimeSpan>();
    }

    /// <summary>
    /// Retrieves an entity given it's id.
    /// </summary>
    public static Timespan Retrieve(int id)
    {
      // Return null if id is smaller than seed and/or increment for autokey
      if (id < 1)
      {
        return null;
      }
      try
      {
        Key key = new Key(typeof (Timespan), true, "idTimespan", id);
        return Broker.RetrieveInstance<Timespan>(key);
      }
      catch
      {
        return null;
      }
    }

    /// <summary>
    /// Retrieves an entity given it's id, using Gentle.Framework.Key class.
    /// This allows retrieval based on multi-column keys.
    /// </summary>
    public static Timespan Retrieve(Key key)
    {
      return Broker.RetrieveInstance<Timespan>(key);
    }

    /// <summary>
    /// Retrieves a list of Timespan's with the same KeywordID.
    /// </summary>
    public static IList<Timespan> RetrieveTimeSpanList(int KeywordID)
    {
      if (KeywordID < 1)
      {
        return null;
      }
      try
      {
        Key key = new Key(typeof (Timespan), true, "idKeyword", KeywordID);
        return Broker.RetrieveList<Timespan>(key);
      }
      catch
      {
        return null;
      }
    }

    /// <summary>
    /// Persists the entity if it was never persisted or was changed.
    /// </summary>
    public override void Persist()
    {
      if (IsChanged || !IsPersisted)
      {
        try
        {
          base.Persist();
        }
        catch (Exception ex)
        {
          Log.Error("Exception in Timespan.Persist() with Message {0}", ex.Message);
          return;
        }
        isChanged = false;
      }
    }

    #endregion

    #region Relations

    #endregion

    #region base class overrides

    public override string ToString()
    {
      return String.Format("from {0} to {1}", StartTime, EndTime);
    }

    #endregion
  }
}