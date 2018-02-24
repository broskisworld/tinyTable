# tinyTable
A small c# library for displaying 2-dimensional tables in console applications.

# Use
To use this library, you must add a reference to tinyTable.dll and include the namespace tinyTable into your program.
TinyTable<T> is the only class in the namespace.
The table can be filled with any data type that contains the method ToString(), and that data type must be set at the time of class initialization. If multiple data types are to be displayed in one table, conversion is required before adding to the table.

The class TinyTable<T> has 3 constructors:
'TinyTable()'
Default constructor. In order to display the table, a defined number of columns is REQUIRED. Either resize() or rename() must be called before getDrawable() can be safely invoked.

'TinyTable(int _numColumns)'
Defines a set number of columns, but no titles for the columns.

'TinyTable(params string[] _columnDescriptors)'
Defines a set number and name for the columns.

Two methods can be called to adjust the size of the column:
'void resize(int _numColumns)'
Adds or modifies the number of columns in the table.

'void rename(params string[] _columnDescriptors)'
Adds or modifies the number and names for the columns.

Once a set number of columns is defined, data can be added to the table:
'void addLine(params T[] newLine)'
Adds a new row of data to the table. WARNING: if the number of paramaters passed in is not equal to the number of columns separately, no errors will be thrown, but the method call will simply be ignored.

After the table is complete, you are ready to draw!
'string getDrawable()'
Returns a string with the drawn table inside of it. Can be slightly time-consuming for large tables. If it's necessary to display the same table many times, calling it once and saving the output is recommended over calling getDrawable() repeatedly.

# Example
The following example displays the growth of bacteria in a petri dish using TinyTable:
'''c#
using System;

using tinyTable;

namespace Example
{
  class Program
  {
    static void Main(string[] args)
    {
      TinyTable<int> experimentData = new TinyTable<int>("Hour", "# of E. Coli");
      const int GENERATION_TIME_MIN = 17;
      const int EXPERIMENT_LENGTH_HR = 24;
      
      for(int curHour = 0; curHour <= EXPERIMENT_LENGTH_HR; curHour++)
      {
        int numBacteria = (int)Math.Pow(2, (curHour * 60) / GENERATION_TIME_MIN);
        
        experimentData.addLine(curHour, numBacteria);
      }
      
      Console.Write(experimentData.getDrawable());
      
      Console.Write("\nPress any key to exit\n");
      Console.ReadKey();
    }
  }
}
'''

Output:
'''
+----+------------+
|Hour|# of E. Coli|
+----+------------+
|0   |1           |
|1   |11          |
|2   |133         |
|3   |1539        |
|4   |17776       |
|5   |205255      |
|6   |2370024     |
|7   |27366021    |
|8   |315987909   |
+----+------------+

Press any key to exit
'''
