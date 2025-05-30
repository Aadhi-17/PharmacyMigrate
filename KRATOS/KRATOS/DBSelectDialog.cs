using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static KRATOS.DataAcessLayer.GetDB;

namespace KRATOS
{
    public partial class DBSelectDialog : Form
    {
        public DBSelectDialog()
        {
            InitializeComponent();
        }

        string dbname = "";
        string server = "";
        string shortName = "";
        string conversionNo = "";

        #region FORM LOAD



        public DBSelectDialog(string server)
        {
            InitializeComponent();
            this.server = server;
        }

        private void DBSelectDialog_Load(object sender, System.EventArgs e)
        {
#if DEBUG
            addDatatoListBox(server, $@"");
            if (lxtbxDataBase.Items.Count == 0)
            {
                addDatatoListBox(server, "");
            }
#endif
        }

        #endregion

        #region Form Events

        private void lxtbxDataBase_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            lxtbxDataBase.DoubleClick += new EventHandler(lxtbxDataBase_DoubleClick);
            lxtbxDataBase.KeyDown += new KeyEventHandler(lxtbxDataBase_KeyDown);
        }

        private void lxtbxDataBase_DoubleClick(object sender, EventArgs e)
        {
            if (lxtbxDataBase.SelectedItem != null)
            {
                select();
            }
        }

        private void txtDataBase_TextChanged_1(object sender, EventArgs e)
        {
            addDatatoListBox(server, txtDataBase.Text.ToString());
        }

        private void bttnChoose_Click(object sender, EventArgs e)
        {
            select();
        }

        private void lxtbxDataBase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                select();
            }
        }

        private void bttnSerach_Click_1(object sender, EventArgs e)
        {
            addDatatoListBox(server, txtDataBase.Text.ToString());
        }



        #endregion

        #region Helper Methods

        public void addDatatoListBox(string server, string text)
        {
            List<string> dbList = new List<string>();
            dbList = Get_Databse(Connection_String(server.ToString().ToLower(), "master"), text);
            lxtbxDataBase.Items.Clear();
            foreach (string db in dbList)
            {
                lxtbxDataBase.Items.Add(db);
            }
        }

        public void select()
        {
            dbname = lxtbxDataBase.SelectedItem.ToString();
            this.Close();
        }

        public string dbName
        {
            get { return dbname; }
        }

        #endregion
    }
}
