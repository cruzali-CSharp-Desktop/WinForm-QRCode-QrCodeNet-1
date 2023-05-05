using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneraQR_01
{
    public partial class GeneraQRForm : Form
    {
        public GeneraQRForm()
        {
            InitializeComponent();
            BtnGuardar.Enabled = false;
        }

        private void BtnGenerar_Click(object sender, EventArgs e)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(TxbTexto.Text.Trim(), out qrCode);

            GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(400,QuietZoneModules.Zero), Brushes.Black, Brushes.White);

            MemoryStream memoryStream = new MemoryStream();
            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, memoryStream);
            //var imageTMP = new Bitmap(imageTMP, new Size(new Point(200, 200)));

            var imageTMP = new Bitmap(memoryStream);
            var imagen = new Bitmap(imageTMP, new Size(new Point(200, 200)));
            pictureBox1.BackgroundImage = imagen;

            BtnGuardar.Enabled = true;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            using (Image image = (Image)pictureBox1.BackgroundImage.Clone())
            {
                SaveFileDialog resDialog = new SaveFileDialog();
                resDialog.AddExtension = true;
                resDialog.Filter = "Image PNG (*.png)|*.png";

                if (resDialog.ShowDialog() == DialogResult.OK)
                {
                    image.Save(resDialog.FileName, ImageFormat.Png);
                }
            }
        }
    }
}
