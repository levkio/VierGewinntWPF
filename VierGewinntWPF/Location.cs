using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinntWPF
{
    class Location
    {
        public int Column { get; set;}
        public int Row { get; set;}

        public Location(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public override string ToString()
        {
            return "Column: " + Column + " Row: " + Row;
        }

        /// <summary>
        /// checks if the row & column got the same values
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var t = obj as Location;
            if (t == null) return false;
            return this.Column == t.Column && this.Row == t.Row;
        }
        public override int GetHashCode()
        {
            return Column.GetHashCode() + Row.GetHashCode();
        }
    }
}
