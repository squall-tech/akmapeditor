using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Brush = AKMapEditor.OtMapEditor.OtBrush.Brush;
using AKMapEditor.OtMapEditor;

namespace AKMapEditor
{
    public partial class FindItemForm : Form
    {

        public Brush selectedBrush = null;
        private List<Brush> brushList = null;
        private ListView ItemListView;

        public FindItemForm()
        {
            InitializeComponent();
            CreateListView();
            ItemListView.HideSelection = true;
        }

        public void CreateListView()
        {
            columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader.Width = 165;
            ItemListView = new ListView();
            ItemListView.SmallImageList = Global.gfx.imageItemList;
            ItemListView.Dock = DockStyle.Fill;
            ItemListView.HeaderStyle = ColumnHeaderStyle.None;
            ItemListView.FullRowSelect = true;
            ItemListView.HideSelection = false;
            ItemListView.MultiSelect = false;
            ItemListView.Name = "ItemListView";
            ItemListView.View = System.Windows.Forms.View.Details;
            ItemListView.SelectedIndexChanged += new System.EventHandler(ListView_SelectedIndexChanged);
            ItemListView.MouseDoubleClick += ListDBClick;
            panelEditor.Controls.Add(ItemListView);
        }

        private void ListDBClick(object sender, MouseEventArgs e)
        {
            if (selectedBrush != null)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void ClearListView()
        {
            ItemListView.Clear();
            ItemListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnHeader });
        }

        private void ShowItems(String text)
        {
            try
            {
                ClearListView();
                List<Brush> filteredList = brushList.Where(x => x.getName().ToLower().Contains(text.ToLower())).ToList();
                ItemListView.BeginUpdate();
                foreach (Brush brush in filteredList)
                {
                    ItemListView.Items.Add(CreateListViewItem(brush));
                }
                ItemListView.EndUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        public ListViewItem CreateListViewItem(Brush brush)
        {
            ListViewItem lvItem = new ListViewItem();
            lvItem.Text = brush.getName() + ":" + brush.getLookID();
            if (brush.getLookID() != 0)
            {
                int lookid = brush.getLookID();
                lvItem.ImageIndex = Global.gfx.getImageIndex(Global.items.items[lookid].SpriteId);
            }
            if (brush.getSpriteLookID() != 0)
            {
                lvItem.ImageIndex = Global.gfx.getCreatureImageIndex((int)brush.getSpriteLookID());
            }
            lvItem.Tag = brush;
            return lvItem;
        }

        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (ItemListView.SelectedItems.Count > 0)
            {
                selectedBrush = (Brush)ItemListView.SelectedItems[0].Tag;             
            }

        }

        public void setBrushList(List<Brush> list)
        {
            brushList = list;
        }

        private void cancelBT_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void okBT_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        public Brush getSelectedBrush()
        {
            return selectedBrush;
        }

        private void stringTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!"".Equals(stringTextBox) && stringTextBox.TextLength > 1)
            {
                ShowItems(stringTextBox.Text);
            }
            else
            {
                ClearListView();
            }
        }
    }
}
