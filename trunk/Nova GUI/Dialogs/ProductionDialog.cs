// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Dialog to manipulate a planet's production queue.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using NovaCommon;
using NovaClient;

namespace Nova
{

// ============================================================================
// Production queue dialog.
// ============================================================================

   public class ProductionDialog : System.Windows.Forms.Form
   {


// ----------------------------------------------------------------------------
// Non-designer generated data items
// ----------------------------------------------------------------------------

      private Star       QueueStar = null;
      private ClientState   StateData = null;
      private Intel TurnData  = null;


// ----------------------------------------------------------------------------
// Desginger generated varaiables
// ----------------------------------------------------------------------------

      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.Button AddToQueue;
      private System.Windows.Forms.ListView DesignList;
      private System.Windows.Forms.ListView QueueList;
      private System.Windows.Forms.Button OK;
      private System.Windows.Forms.ColumnHeader Description;
      private System.Windows.Forms.Button Cancel;
      private System.Windows.Forms.ColumnHeader QueueDescription;
      private System.Windows.Forms.ColumnHeader QueueQuantity;
      private System.Windows.Forms.GroupBox groupBox3;
      //private IContainer components;
      private System.ComponentModel.Container components;
      private System.Windows.Forms.GroupBox groupBox4;
      private ControlLibrary.ResourceDisplay DesignCost;
      private Button RemoveFromQueue;
      private ControlLibrary.ResourceDisplay ProductionCost;


// ============================================================================
// Dialog constrution.
// ============================================================================

      public ProductionDialog(Star star)
      {
         QueueStar  = star;
         StateData  = ClientState.Data;
         TurnData   = StateData.InputTurn;

         InitializeComponent();
      }


// ============================================================================
// Clean up resources.
// ============================================================================

      protected override void Dispose(bool disposing)
      {
         if (disposing)
         {
            if (components != null)
            {
               components.Dispose();
            }
         }
         base.Dispose(disposing);
      }


// ============================================================================
// Component designer code
// ============================================================================

      #region Windows Form Designer generated code
      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductionDialog));
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.DesignList = new System.Windows.Forms.ListView();
         this.Description = new System.Windows.Forms.ColumnHeader();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.QueueList = new System.Windows.Forms.ListView();
         this.QueueDescription = new System.Windows.Forms.ColumnHeader();
         this.QueueQuantity = new System.Windows.Forms.ColumnHeader();
         this.AddToQueue = new System.Windows.Forms.Button();
         this.OK = new System.Windows.Forms.Button();
         this.Cancel = new System.Windows.Forms.Button();
         this.groupBox3 = new System.Windows.Forms.GroupBox();
         this.DesignCost = new ControlLibrary.ResourceDisplay();
         this.groupBox4 = new System.Windows.Forms.GroupBox();
         this.ProductionCost = new ControlLibrary.ResourceDisplay();
         this.RemoveFromQueue = new System.Windows.Forms.Button();
         this.groupBox1.SuspendLayout();
         this.groupBox2.SuspendLayout();
         this.groupBox3.SuspendLayout();
         this.groupBox4.SuspendLayout();
         this.SuspendLayout();
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.DesignList);
         this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.groupBox1.Location = new System.Drawing.Point(8, 8);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(256, 328);
         this.groupBox1.TabIndex = 0;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Available Designs";
         // 
         // DesignList
         // 
         this.DesignList.AutoArrange = false;
         this.DesignList.BackColor = System.Drawing.SystemColors.ControlLightLight;
         this.DesignList.CausesValidation = false;
         this.DesignList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Description});
         this.DesignList.FullRowSelect = true;
         this.DesignList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
         this.DesignList.HideSelection = false;
         this.DesignList.Location = new System.Drawing.Point(8, 16);
         this.DesignList.MultiSelect = false;
         this.DesignList.Name = "DesignList";
         this.DesignList.Size = new System.Drawing.Size(240, 304);
         this.DesignList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.DesignList.TabIndex = 0;
         this.DesignList.TabStop = false;
         this.DesignList.UseCompatibleStateImageBehavior = false;
         this.DesignList.View = System.Windows.Forms.View.Details;
         this.DesignList.SelectedIndexChanged += new System.EventHandler(this.AvailableSelected);
         // 
         // Description
         // 
         this.Description.Text = "Description";
         this.Description.Width = 236;
         // 
         // groupBox2
         // 
         this.groupBox2.Controls.Add(this.QueueList);
         this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.groupBox2.Location = new System.Drawing.Point(328, 8);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(256, 328);
         this.groupBox2.TabIndex = 1;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "Production Queue";
         // 
         // QueueList
         // 
         this.QueueList.AutoArrange = false;
         this.QueueList.BackColor = System.Drawing.SystemColors.ControlLightLight;
         this.QueueList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.QueueDescription,
            this.QueueQuantity});
         this.QueueList.FullRowSelect = true;
         this.QueueList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
         this.QueueList.HideSelection = false;
         this.QueueList.Location = new System.Drawing.Point(8, 16);
         this.QueueList.MultiSelect = false;
         this.QueueList.Name = "QueueList";
         this.QueueList.Size = new System.Drawing.Size(240, 304);
         this.QueueList.TabIndex = 0;
         this.QueueList.TabStop = false;
         this.QueueList.UseCompatibleStateImageBehavior = false;
         this.QueueList.View = System.Windows.Forms.View.Details;
         this.QueueList.SelectedIndexChanged += new System.EventHandler(this.QueueSelected);
         // 
         // QueueDescription
         // 
         this.QueueDescription.Text = "Description";
         this.QueueDescription.Width = 182;
         // 
         // QueueQuantity
         // 
         this.QueueQuantity.Text = "Quantity";
         this.QueueQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
         this.QueueQuantity.Width = 54;
         // 
         // AddToQueue
         // 
         this.AddToQueue.Enabled = false;
         this.AddToQueue.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.AddToQueue.Location = new System.Drawing.Point(272, 120);
         this.AddToQueue.Name = "AddToQueue";
         this.AddToQueue.Size = new System.Drawing.Size(48, 24);
         this.AddToQueue.TabIndex = 2;
         this.AddToQueue.Text = "Add";
         this.AddToQueue.Click += new System.EventHandler(this.AddToQueue_Click);
         // 
         // OK
         // 
         this.OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.OK.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.OK.Location = new System.Drawing.Point(440, 440);
         this.OK.Name = "OK";
         this.OK.Size = new System.Drawing.Size(64, 24);
         this.OK.TabIndex = 3;
         this.OK.Text = "OK";
         this.OK.Click += new System.EventHandler(this.OK_Click);
         // 
         // Cancel
         // 
         this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.Cancel.Location = new System.Drawing.Point(520, 440);
         this.Cancel.Name = "Cancel";
         this.Cancel.Size = new System.Drawing.Size(64, 24);
         this.Cancel.TabIndex = 4;
         this.Cancel.Text = "Cancel";
         // 
         // groupBox3
         // 
         this.groupBox3.Controls.Add(this.DesignCost);
         this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.groupBox3.Location = new System.Drawing.Point(8, 344);
         this.groupBox3.Name = "groupBox3";
         this.groupBox3.Size = new System.Drawing.Size(256, 88);
         this.groupBox3.TabIndex = 5;
         this.groupBox3.TabStop = false;
         this.groupBox3.Text = "Design Cost";
         // 
         // DesignCost
         // 
         this.DesignCost.Location = new System.Drawing.Point(8, 16);
         this.DesignCost.Name = "DesignCost";
         this.DesignCost.Size = new System.Drawing.Size(240, 64);
         this.DesignCost.TabIndex = 0;
         this.DesignCost.Value = ((NovaCommon.Resources)(resources.GetObject("DesignCost.Value")));
         // 
         // groupBox4
         // 
         this.groupBox4.Controls.Add(this.ProductionCost);
         this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.groupBox4.Location = new System.Drawing.Point(328, 344);
         this.groupBox4.Name = "groupBox4";
         this.groupBox4.Size = new System.Drawing.Size(256, 88);
         this.groupBox4.TabIndex = 6;
         this.groupBox4.TabStop = false;
         this.groupBox4.Text = "Production Cost";
         // 
         // ProductionCost
         // 
         this.ProductionCost.Location = new System.Drawing.Point(8, 16);
         this.ProductionCost.Name = "ProductionCost";
         this.ProductionCost.Size = new System.Drawing.Size(240, 64);
         this.ProductionCost.TabIndex = 0;
         this.ProductionCost.Value = ((NovaCommon.Resources)(resources.GetObject("ProductionCost.Value")));
         // 
         // RemoveFromQueue
         // 
         this.RemoveFromQueue.Enabled = false;
         this.RemoveFromQueue.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.RemoveFromQueue.Location = new System.Drawing.Point(273, 224);
         this.RemoveFromQueue.Name = "RemoveFromQueue";
         this.RemoveFromQueue.Size = new System.Drawing.Size(48, 24);
         this.RemoveFromQueue.TabIndex = 7;
         this.RemoveFromQueue.Text = "Remove";
         this.RemoveFromQueue.Click += new System.EventHandler(this.RemoveFromQueue_Click);
         // 
         // ProductionDialog
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(594, 472);
         this.Controls.Add(this.RemoveFromQueue);
         this.Controls.Add(this.groupBox4);
         this.Controls.Add(this.groupBox3);
         this.Controls.Add(this.Cancel);
         this.Controls.Add(this.OK);
         this.Controls.Add(this.AddToQueue);
         this.Controls.Add(this.groupBox2);
         this.Controls.Add(this.groupBox1);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.Name = "ProductionDialog";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "Production Queue";
         this.Load += new System.EventHandler(this.OnLoad);
         this.groupBox1.ResumeLayout(false);
         this.groupBox2.ResumeLayout(false);
         this.groupBox3.ResumeLayout(false);
         this.groupBox4.ResumeLayout(false);
         this.ResumeLayout(false);

      }
      #endregion


// ============================================================================
// Populate the available designs items list box with the things we can build.
// Generally, we can build designs created by the player. However, we only
// include ships in the list if the star has a starbase with enough dock
// capacity to build the design. 
// ============================================================================

      private void OnLoad(object sender, System.EventArgs e)
      {
         DesignList.BeginUpdate();

         Fleet starbase     = QueueStar.Starbase;
         int  dockCapacity = 0;

         if (starbase != null) {
            dockCapacity = starbase.CargoCapacity;
         }

         foreach (Design design in TurnData.AllDesigns.Values) {
            if (design.Owner == ClientState.Data.RaceName || design.Owner == "*"){

               if (design.Type == "Ship") {
                  if (dockCapacity > design.Mass) {
                     DesignList.Items.Add(new ListViewItem(design.Name));
                  }
               }
               else {
                  DesignList.Items.Add(new ListViewItem(design.Name));
               }
            }
         }

        DesignList.EndUpdate();

        Nova.QueueList.Populate(this.QueueList, QueueStar.ManufacturingQueue);
        UpdateProductionCost();
      }


// ============================================================================
// ReadIntel a design being selected for possible construction.
// ============================================================================

      private void AvailableSelected(object sender, System.EventArgs e)
      {
         if (DesignList.SelectedItems.Count <= 0) return;

         string name = DesignList.SelectedItems[0].Text;

         Design design =
            TurnData.AllDesigns[StateData.RaceName + "/" + name] as Design;
         
         DesignCost.Value = design.Cost;

         if (AddToQueue.Enabled == false) {
            AddToQueue.Enabled = true;
         }
      }


// ============================================================================
// Production queue item selected changed.
// ============================================================================

      private void QueueSelected(object sender, EventArgs e)
      {
         if (QueueList.SelectedItems.Count > 0) {
            RemoveFromQueue.Enabled = true;
         }
         else {
            RemoveFromQueue.Enabled = false;
         }
      }


// ============================================================================
// Add to queue button pressed.
// ============================================================================

      private void AddToQueue_Click(object sender, System.EventArgs e)
      {
         if (DesignList.SelectedItems.Count <= 0) return;

         String name   = DesignList.SelectedItems[0].Text;
         Design design =
            TurnData.AllDesigns[StateData.RaceName + "/" + name] as Design;

         // Starbases are handled differently to the other component types.
         if (design.Type == "Starbase") {
            AddStarbase(design);
            return;
         }

         // Ctrl-Add + 100 items
         // shift-Add +10 items
         // Add +1 items
         if (Button.ModifierKeys == Keys.Control)
         {
             AddDesign(design, 100);
         } 
         else if (Button.ModifierKeys == Keys.Shift)
         {
             AddDesign(design, 10);
         }
         else {
            AddDesign(design, 1);
         }
      }


// ============================================================================
// Remove from queue button pressed.
// ============================================================================

      private void RemoveFromQueue_Click(object sender, EventArgs e)
      {
         if (QueueList.SelectedItems.Count > 0) {
            QueueList.Items.RemoveAt(QueueList.SelectedIndices[0]);
            UpdateProductionCost();
         }
      }


// ============================================================================
// Add a selected item into the production queue. If no item is selected in
// the queue, add the new one on the end. If an item is selected and it is the
// same type as the one being added then just increment the quantity.
// Otherwise, just add the item on at the end of the queue.
// ============================================================================

      private void AddDesign(Design design, int quantity)
      {
         ListViewItem itemToAdd = new ListViewItem();
         ListViewItem itemAdded;

         itemToAdd.Text = design.Name;
         itemToAdd.Tag  = design;
         itemToAdd.SubItems.Add(quantity.ToString(System.Globalization.CultureInfo.InvariantCulture));

         if (QueueList.SelectedItems.Count == 0) {
            itemAdded = QueueList.Items.Add(itemToAdd);
            itemToAdd.Selected = true;
         }
         else {
            int s = QueueList.SelectedIndices[0];

            if (design.Name == QueueList.Items[s].Text) {
               int total = quantity;
               total += Convert.ToInt32(QueueList.Items[s].SubItems[1].Text);
               QueueList.Items[s].SubItems[1].Text = total.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
            else {
               QueueList.Items[s].Selected = false;
               itemAdded = QueueList.Items.Add(itemToAdd);
               itemAdded.Selected = true;
            }
         }

         UpdateProductionCost();
      }


      /// ============================================================================
      /// <summary> Add a starbase to the production queue. </summary>
      /// Starbases are special in that there
      /// can ony ever be one in the production queue, no matter how many he tries to
      /// add.
      /// TODO (priority 3) ??? What if I want to build a small base first, then add 
      ///   a larger base latter. I can queue two different base designs in Stars! ???
      /// ============================================================================
      private void AddStarbase(Design design)
      {
         // First run through the production queue to see if there is already a
         // starbase there. If there is remove it.

          foreach (ListViewItem item in QueueList.Items)
          {
              Design thisDesign = item.Tag as Design;
              if (thisDesign != null) //factories and defenses and mines dont have a design...
              {
                  if (thisDesign.Type == "Starbase")
                  {
                      item.Remove();
                  }
              }
          }

         AddDesign(design, 1);
      }


// ============================================================================
// Update production queue cost
// ============================================================================

      private void UpdateProductionCost()
      {
         NovaCommon.Resources cost = new NovaCommon.Resources();

         foreach (ListViewItem item in QueueList.Items)
         {
            string name   = item.Text;

            Design design = TurnData.AllDesigns
                            [StateData.RaceName + "/" + name] as Design;

            if (design == null)
            {
                Report.FatalError("ProducationDialog.cs UpdateProducionCost() - Design \"" + StateData.RaceName + "/" + name + "\" no longer exists.");
            }

            int quantity  = Convert.ToInt32(item.SubItems[1].Text);

            cost.Ironium   += design.Cost.Ironium   * quantity;
            cost.Boranium  += design.Cost.Boranium  * quantity;
            cost.Germanium += design.Cost.Germanium * quantity;
            cost.Energy    += design.Cost.Energy    * quantity;
         }

         ProductionCost.Value = cost;
      }


// ============================================================================
// OK button pressed.
// ============================================================================

      private void OK_Click(object sender, System.EventArgs e)
      {
         QueueStar.ManufacturingQueue.Queue.Clear();

         foreach (ListViewItem i in QueueList.Items)
         {
            ProductionQueue.Item item   = new ProductionQueue.Item();

            item.Name       = i.SubItems[0].Text;
            item.Quantity   = Convert.ToInt32(i.SubItems[1].Text);

            Design design = TurnData.AllDesigns
                            [StateData.RaceName + "/" + item.Name] as Design;
            
            item.BuildState = design.Cost;

            QueueStar.ManufacturingQueue.Queue.Add(item);
         }

         Close();
      }

   }

}
