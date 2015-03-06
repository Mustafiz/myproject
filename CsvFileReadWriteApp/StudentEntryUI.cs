using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.Windows.Forms;

namespace CsvFileReadWriteApp
{
    public partial class StudentEntryUI : Form
    {
        private string fileLocation = "E:\\student-info.csv";
        public StudentEntryUI()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Student aStudent = new Student();
            aStudent.RegNo = regNoTextBox.Text;
            aStudent.Name = nameTextBox.Text;
            aStudent.Email = emailTextBox.Text;

            string aStudentInfoString = aStudent.RegNo + "," + aStudent.Name + "," + aStudent.Email;

            FileStream aFileStream = new FileStream(fileLocation, FileMode.Append);
            StreamWriter aStreamWriter = new StreamWriter(aFileStream);
            aStreamWriter.Write(aStudentInfoString);
            aStreamWriter.WriteLine();
            aStreamWriter.Close();
            aFileStream.Close();

            regNoTextBox.Text = string.Empty;
            nameTextBox.Text = string.Empty;
            emailTextBox.Text = string.Empty;
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            studentListView.Items.Clear();
            FileStream aFileStream = new FileStream(fileLocation, FileMode.Open);
            StreamReader aStreamReader = new StreamReader(aFileStream);

            List<string> aList = new List<string>();

            while (!aStreamReader.EndOfStream)
            {
                string aLine = aStreamReader.ReadLine();
                aList.Add(aLine);
            }

            foreach (string item in aList)
            {
                string[] studentProperties = item.Split(',');
                ListViewItem aListViewItem = new ListViewItem(studentProperties[0]);
                aListViewItem.SubItems.Add(studentProperties[1]);
                aListViewItem.SubItems.Add(studentProperties[2]);
                studentListView.Items.Add(aListViewItem);
            }

            aStreamReader.Close();
            aFileStream.Close();
        }
    }
}
