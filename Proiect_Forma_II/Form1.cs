using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibrarieModele;
using NivelAccesDate;
using System.Collections;

namespace Proiect_Forma_II
{
    public partial class Form1 : Form
    {
        IStocareData adminMasini;
        public Form1()
        {
            InitializeComponent();
            adminMasini = StocareFactory.GetAdministratorStocare();
        }

        private void btnAdauga_Click(object sender, EventArgs e)
        {
            Masina s;
            txtNumeVanzator.ForeColor = Color.Black;
            txtNumeCumparator.ForeColor = Color.Black;
            txtTip.ForeColor = Color.Black;
            txtAnFabricare.ForeColor = Color.Black;
            txtDataTranzactie.ForeColor = Color.Black;
            txtPret.ForeColor = Color.Black;
            txtOptiuni.ForeColor = Color.Black;
            txtCuloare.ForeColor = Color.Black;
            CodEroare valideaza = Validare(txtNumeVanzator.Text, txtNumeCumparator.Text, txtTip.Text, txtAnFabricare.Text, txtDataTranzactie.Text, txtPret.Text, txtOptiuni.Text, txtCuloare.Text);
            if (valideaza != CodEroare.CORECT)
            {
                switch (valideaza)
                {
                    case CodEroare.NUMEVANZATOR_INCORECT:
                        txtNumeVanzator.ForeColor = Color.Red;
                        break;
                    case CodEroare.NUMECUMPARATOR_INCORECT:
                        txtNumeCumparator.ForeColor = Color.Red;
                        break;
                    case CodEroare.TIP_INCORECTE:
                        txtTip.ForeColor = Color.Red;
                        break;
                    case CodEroare.ANFABRICARE_INCORECT:
                        txtAnFabricare.ForeColor = Color.Red;
                        break;
                    case CodEroare.DATATRANZACTIE_INCORECT:
                        txtDataTranzactie.ForeColor = Color.Red;
                        break;
                    case CodEroare.PRET_INCORECTE:
                        txtPret.ForeColor = Color.Red;
                        break;
                    case CodEroare.OPTIUNE_INCORECT:
                        txtOptiuni.ForeColor = Color.Red;
                        break;
                    case CodEroare.CULOARE_INCORECT:
                        txtCuloare.ForeColor = Color.Red;
                        break;
                }
            }
            else
            {
                s = new Masina(txtNumeVanzator.Text, txtNumeCumparator.Text, txtTip.Text, txtAnFabricare.Text, txtDataTranzactie.Text, txtPret.Text, txtOptiuni.Text, txtCuloare.Text);
                adminMasini.AddMasina(s);
                lblAdauga.Text = "Masina a fost adaugata";
            }
        }
        private CodEroare Validare(string numeVanzator, string numeCumparator, string Tip, string AnFabricare, string DataTranzactie, string Pret, string Optiuni, string Culoare)
        {
            if (numeVanzator == string.Empty)
            {
                return CodEroare.NUMEVANZATOR_INCORECT;
            }
            if (numeCumparator == string.Empty)
            {
                return CodEroare.NUMECUMPARATOR_INCORECT;
            }
            if (Tip == string.Empty)
            {
                return CodEroare.TIP_INCORECTE;
            }
            if (AnFabricare == string.Empty)
            {
                return CodEroare.ANFABRICARE_INCORECT;
            }
            if (DataTranzactie == string.Empty)
            {
                return CodEroare.DATATRANZACTIE_INCORECT;
            }
            if (Pret == string.Empty)
            {
                return CodEroare.PRET_INCORECTE;
            }
            if (Optiuni == string.Empty)
            {
                return CodEroare.OPTIUNE_INCORECT;
            }
            if (Culoare == string.Empty)
            {
                return CodEroare.CULOARE_INCORECT;
            }
            return CodEroare.CORECT;
        }

        private void btnAfiseaza_Click(object sender, EventArgs e)
        {
            rtbAfisare.Clear();
            ArrayList masini = adminMasini.GetMasini();
            foreach (Masina s in masini)
            {
                rtbAfisare.AppendText(s.IdMasina.ToString());
                rtbAfisare.AppendText(" ");
                rtbAfisare.AppendText(s.NumeVanzator);
                rtbAfisare.AppendText(" ");
                rtbAfisare.AppendText(s.NumeCumparator);
                rtbAfisare.AppendText(" ");
                rtbAfisare.AppendText(s.Tip);
                rtbAfisare.AppendText(" ");
                rtbAfisare.AppendText(s.AnFabricare);
                rtbAfisare.AppendText(" ");
                rtbAfisare.AppendText(s.DataTranzactie);
                rtbAfisare.AppendText(" ");
                rtbAfisare.AppendText(s.Pret);
                rtbAfisare.AppendText(" ");
                rtbAfisare.AppendText(s.Optiune);
                rtbAfisare.AppendText(" ");
                rtbAfisare.AppendText(s.Culoare);
                rtbAfisare.AppendText(Environment.NewLine);
            }
        }

        private void btnCauta_Click(object sender, EventArgs e)
        {
            Masina s = adminMasini.GetMasina(txtNumeVanzator.Text, txtTip.Text);
            if (s != null)
            {
                lblCauta.Text = s.ConversiaLaSir_PentruFisier_PentruForma();
            }
            else
                lblCauta.Text = "Nu s-a gasit masina";
            if (txtNumeVanzator.Enabled == true && txtTip.Enabled == true)
            {
                txtNumeVanzator.Enabled = false;
                txtTip.Enabled = false;
            }
            else
            {
                txtNumeVanzator.Enabled = true;
                txtTip.Enabled = true;
            }
        }

        private void btnModifica_Click(object sender, EventArgs e)
        {
            Masina s = adminMasini.GetMasina(txtNumeVanzator.Text, txtTip.Text);
            if (s != null)
            {

                s.NumeCumparator=txtNumeCumparator.Text;
                s.AnFabricare=txtAnFabricare.Text;
                s.DataTranzactie=txtDataTranzactie.Text;
                s.Pret=txtPret.Text;
                s.Optiune=txtOptiuni.Text;
                s.Culoare=txtCuloare.Text;

                adminMasini.UpdateMasina(s);
                lblModifica.Text = "Modificare efectuata";
                txtNumeVanzator.Enabled = true;
                txtTip.Enabled = true;
            }
            else
            {
                lblModifica.Text = "Masina inexistenta";
            }
            
        }
    }
}
