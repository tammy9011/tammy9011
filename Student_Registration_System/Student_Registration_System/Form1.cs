using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SLRDbConnector;

namespace Student_Registration_System
{
    public partial class Form1 : Form
    {
        DbConnector db;
        public Form1()
        {
            InitializeComponent();
            db = new DbConnector();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            db.FillCombobox("SELECT name FROM tblClasses", classComboBox);
            db.fillDataGridView("SELECT * FROM tblStudents", dataGridView1);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string class_id;

            if (isFormValid())
            {
                db.getSingleValue("SELECT id FROM tblClasses WHERE name= '" + classComboBox.Text.ToString() + "'", out class_id, 0);
                string abc = db.performCRUD("insert into tblStudents(name,fathers_name,address,date_of_birth,class_id) values('" + txtName.Text + "','" + txtFathersName.Text + "','" + txtAddress.Text + "','" + dtDOB.Text + "','" + class_id + "')");
                this.OnLoad(e);
            }
            else
            {
                MessageBox.Show("Please Fill All Required Fields", "Required Fields Are Empty",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private bool isFormValid()
        {
            if (txtName.Text.ToString().Trim() == string.Empty
                || txtFathersName.Text.ToString().Trim() == string.Empty
                || txtAddress.Text.ToString().Trim() == string.Empty)
            {
                return false;
            }

            return true;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            //exits form
            Application.Exit();
        }
    }
}
