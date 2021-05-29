using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilgeAdamSinav2
{
    public partial class Form1 : Form
    {
        UrunContext db;
        public Form1()
        {
            InitializeComponent();
            
        }
        

    private void Form1_Load(object sender, EventArgs e)
        {
            db = new UrunContext();
            UrunleriYukle();

        }

        void UrunleriYukle()
        {
            lstGoruntule.Items.Clear();

            foreach (Urun item in db.Urunler.ToList())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = item.UrunID.ToString();
                lvi.SubItems.Add(item.UrunAdi);
                lvi.SubItems.Add(String.Format("{0:C2}", item.BirimFiyati));
                lvi.SubItems.Add(item.StokMiktari.ToString() + " Adet ");
                lvi.Tag = item;
                lstGoruntule.Items.Add(lvi);
            }
            if (lstGoruntule.Items.Count == 0)
            {
                btnGuncelle.Enabled = false;
                btnAra.Enabled = false;
            }
        }
        void Temizle()
        {
            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                    item.Text = "";

            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            Urun u = new Urun();            

            int stokMkt = 0; decimal fiyat = 0;         

            try
            {
                stokMkt = int.Parse(txtStokMiktari.Text);
                u.StokMiktari = stokMkt;
                fiyat = decimal.Parse(txtBirimFiyati.Text);
                u.BirimFiyati = fiyat;
            }

            catch
            {
                MessageBox.Show("Stok miktarı ve birim fiyat sayısal değer olmalı!");
                txtStokMiktari.BackColor = Color.Yellow; 
                txtBirimFiyati.BackColor = Color.Yellow;
                return;
            }

            if (txtUrunAdi.Text == "")
            {
                MessageBox.Show("Lütfen ürün adını giriniz.");
                txtUrunAdi.BackColor = Color.Yellow;
            }

            else
            {
                txtUrunAdi.BackColor = Color.White;
                txtStokMiktari.BackColor = Color.White;
                txtBirimFiyati.BackColor = Color.White;

                u.UrunAdi = txtUrunAdi.Text;          
                db.Urunler.Add(u);
                db.SaveChanges();
                UrunleriYukle();
                Temizle();

                MessageBox.Show("Kaydınız başarıyla gerçekleşmiştir.");


                btnAra.Enabled = true;
            }
        
        }
        
        Urun guncellenecek;

        private void cmsGuncelle_Click(object sender, EventArgs e)
        {
            if (lstGoruntule.SelectedItems.Count == 0)
                return;
            else
            {
                btnGuncelle.Enabled = true;
                btnEkle.Enabled = false;
                btnAra.Enabled = true;
                contextMenuStrip1.Items[1].Enabled = false;
                contextMenuStrip1.Items[0].Enabled = false;

                guncellenecek = lstGoruntule.SelectedItems[0].Tag as Urun;
                txtUrunAdi.Text = guncellenecek.UrunAdi;
                txtBirimFiyati.Text = guncellenecek.BirimFiyati.ToString();
                txtStokMiktari.Text = guncellenecek.StokMiktari.ToString();

            }
        }


        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (lstGoruntule.SelectedItems.Count == 0)
            {
                MessageBox.Show("Güncellemek için ürün seçiniz...");
                return;
            }


            guncellenecek = lstGoruntule.SelectedItems[0].Tag as Urun;
            

            guncellenecek.UrunAdi = txtUrunAdi.Text;
            guncellenecek.BirimFiyati = decimal.Parse(txtBirimFiyati.Text);
            guncellenecek.StokMiktari = int.Parse(txtStokMiktari.Text);

            db.SaveChanges();
            UrunleriYukle();
            Temizle();
            btnEkle.Enabled = true;
            btnAra.Enabled = true;
            btnGuncelle.Enabled = false;
        }

        Urun silinecek;
        private void cmsSil_Click(object sender, EventArgs e)
        {
            

            if (lstGoruntule.SelectedItems.Count == 0)
                return;
            else
            {

                silinecek = lstGoruntule.SelectedItems[0].Tag as Urun;
                db.Urunler.Remove(silinecek);
                db.SaveChanges();
                UrunleriYukle();
                btnAra.Enabled = true;
                btnEkle.Enabled = true;
                btnGuncelle.Enabled = true;
            }
            if (lstGoruntule.Items.Count == 0)
            {
                btnGuncelle.Enabled = false;
                btnAra.Enabled = false;
            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            lstGoruntule.Items.Clear();

            if (txtAra.Text == "")
            {
                MessageBox.Show("Lütfen aramak için değer giriniz.");
                UrunleriYukle();
                btnAra.Enabled = true;
                btnGuncelle.Enabled = true;
            }

            else
            {

                foreach (Urun item in db.Urunler.Where(x => x.UrunAdi.Contains(txtAra.Text)).ToList())
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = item.UrunID.ToString();
                    lvi.SubItems.Add(item.UrunAdi);
                    lvi.SubItems.Add(String.Format("{0:C2}", item.BirimFiyati));
                    lvi.SubItems.Add(item.StokMiktari.ToString() + " Adet ");
                    lvi.Tag = item;
                    lstGoruntule.Items.Add(lvi);

                }
            }
            txtAra.Text = "";

        }

        private void lstGoruntule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstGoruntule.Items.Count == 0 || lstGoruntule.SelectedItems.Count <= 0)
            {
                contextMenuStrip1.Items[0].Enabled = false;
                contextMenuStrip1.Items[1].Enabled = false;

                btnGuncelle.Enabled = false;
            }
            else if (lstGoruntule.SelectedItems.Count > 0)
            {
                contextMenuStrip1.Items[0].Enabled = true;
                contextMenuStrip1.Items[1].Enabled = true;
            }
            
        }
     
    }
}
