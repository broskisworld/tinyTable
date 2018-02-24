/* Josh Bosley
 * Project: tinyTable
 * File: TinyTable.cs
 * 2/24/2018
 * 
 * This library can be used to generate simple 2d tables of many data types to be used in a console c# application.
 * 
 * Josh Bosley is the sole author of tinyTable, and the code can be found at https://github.com/broskisworld/tinyTable.
 * I release this software under a Creative Commons Attribution License (BY).
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace tinyTable
{
    public class TinyTable<T>
    {
        private List<string> columnDescriptors;
        private List<List<T>> genericTbl;
        int numColumns;

        //Creates a new TinyTable with no set size. Without calling rename() or resize(), the table cannot be used.
        public TinyTable()
        {
            columnDescriptors = new List<string>(); //Initialize arrays
            genericTbl = new List<List<T>>();

            numColumns = 0; //Set numColumns to a default (bad) amt
        }

        //Creates a new TinyTable with a set size but no column descriptors
        public TinyTable(int _numColumns)
        {
            columnDescriptors = new List<string>();
            genericTbl = new List<List<T>>();

            numColumns = _numColumns;
        }

        //Creates a new TinyTable with set names and number of columns
        public TinyTable(params string[] _columnDescriptors)
        {
            columnDescriptors = new List<string>(_columnDescriptors);
            genericTbl = new List<List<T>>();

            numColumns = columnDescriptors.Count;
        }

        //Adds or modifies the number of columns on the table
        public void resize(int _numColumns)
        {
            numColumns = _numColumns;
        }

        //Adds or modifies the column number and descriptors to match the new titles passed in
        public void rename(params string[] _columnDescriptors)
        {
            columnDescriptors = new List<string>(_columnDescriptors);
            numColumns = columnDescriptors.Count;
        }

        //Adds a new row of data to the table. If the wrong # of paramaters are passed in, the data is ignored
        public void addLine(params T[] newLine)
        {
            if (numColumns == newLine.Count())
                genericTbl.Add(new List<T>(newLine));
            else
                return; //Ignore function call
        }

        public string getDrawable()
        {
            string drawable = "";
            
            //Determine number of columns
            int[] columnWidth = new int[numColumns];

            //Initialize column length to column descriptor length (probably the biggest thing)
            for (int i = 0; i < columnDescriptors.Count; i++) columnWidth[i] = columnDescriptors[i].Length; //If not using descriptors, size will remain initialized at 0

            //Generate string table of every item
            List<List<string>> stringTbl = new List<List<string>>();

            for (int curRow = 0; curRow < genericTbl.Count; curRow++)
            {
                stringTbl.Add(new List<string>(genericTbl[curRow].Count));  //Add new initialized string list
                for (int curColumn = 0; curColumn < genericTbl[curRow].Count; curColumn++)
                {
                    stringTbl[curRow].Add(genericTbl[curRow][curColumn].ToString());    //Convert to string and save in stringTbl

                    if (stringTbl[curRow][curColumn].Length > columnWidth[curColumn])   //Sort through every string to find biggest string for the total column length
                        columnWidth[curColumn] = stringTbl[curRow][curColumn].Length;
                }
            }

            //Draw the final table

            //TOP BOUNDING LINE
            for(int curColumn = 0; curColumn < numColumns; curColumn++)
            {
                drawable += "+";
                for (int i = 0; i < columnWidth[curColumn]; i++) drawable += "-";
            }
            drawable += "+\n";

            //DESCRIPTORS
            if (columnDescriptors.Count != 0)
            {
                for (int curColumn = 0; curColumn < numColumns; curColumn++)
                {
                    drawable += "|";
                    drawable += columnDescriptors[curColumn].PadRight(columnWidth[curColumn]);
                }
                drawable += "|\n";

                //DIVIDING LINE
                for (int curColumn = 0; curColumn < numColumns; curColumn++)
                {
                    drawable += "+";
                    for (int i = 0; i < columnWidth[curColumn]; i++) drawable += "-";
                }
                drawable += "+\n";
            }

            //DISPLAY ROWS
            for (int curRow = 0; curRow < stringTbl.Count; curRow++)
            {
                for (int curColumn = 0; curColumn < stringTbl[curRow].Count; curColumn++)
                {
                    drawable += "|";
                    drawable += stringTbl[curRow][curColumn].PadRight(columnWidth[curColumn]);  //Give padding to every variable based on the biggest member in the column
                }
                drawable += "|\n";
            }

            //BOTTOM BOUNDING LINE
            for (int curColumn = 0; curColumn < numColumns; curColumn++)
            {
                drawable += "+";
                for (int i = 0; i < columnWidth[curColumn]; i++) drawable += "-";
            }
            drawable += "+\n";

            return drawable;
        }

        ~TinyTable()
        {
            
        }
    }
}
