using System.Windows.Forms;
using System.Drawing;
namespace WinFormsAppForto
{
    public partial class Form1 : Form
    {
        private string selectedFontName = "Arial";
        private int selectedFontSize = 12;
        private Color selectedFontColor = Color.Black;
        private PictureBox pictureBox;
        private Point captionLocation = new Point(200, 150);
        private Font captionFont = new Font("Arial", 12);




        public Form1()
        {
            InitializeComponent();
            InitializeUI();
        }
        private void InitializeUI()
        {
            Button browseButton = new Button
            {
                Text = "Выбрать фото",
                Location = new Point(10, 10),
                Size = new Size(120, 30)
            };
            browseButton.Click += BrowseButton_Click;
            Controls.Add(browseButton);
            pictureBox = new PictureBox
            {
                Location = new Point(10, 50),
                Size = new Size(400, 300),
                BorderStyle = BorderStyle.FixedSingle
            };
            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseUp += PictureBox_MouseUp;
            Controls.Add(pictureBox);
            Label fontLabel = new Label
            {
                Text = "Шрифт:",
                Location = new Point(10, 360)
            };
            Controls.Add(fontLabel);

            ComboBox fontComboBox = new ComboBox
            {
                Location = new Point(70, 360),
                Size = new Size(120, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            fontComboBox.Items.AddRange(FontFamily.Families);
            fontComboBox.SelectedIndexChanged += FontComboBox_SelectedIndexChanged;
            Controls.Add(fontComboBox);
           

            Label fontSizeLabel = new Label
            {
                Text = "Размер шрифта:",
                Location = new Point(200, 360)
            };
            Controls.Add(fontSizeLabel);
            NumericUpDown fontSizeNumericUpDown = new NumericUpDown
            {
                Location = new Point(290, 360),
                Size = new Size(60, 20),
                Minimum = 1,
                Maximum = 100,
                Value = selectedFontSize
            };
            fontSizeNumericUpDown.ValueChanged += FontSizeNumericUpDown_ValueChanged;
            Controls.Add(fontSizeNumericUpDown);

            Label fontColorLabel = new Label
            {
                Text = "Цвет текста:",
                Location = new Point(10, 390)
            };
            Controls.Add(fontColorLabel);

            Button fontColorButton = new Button
            {
                Location = new Point(90, 385),
                Size = new Size(30, 30),
                BackColor = selectedFontColor,
                FlatStyle = FlatStyle.Flat
            };
            fontColorButton.FlatAppearance.BorderSize = 0;
            fontColorButton.Click += FontColorButton_Click;
            Controls.Add(fontColorButton);

            Label captionLabel = new Label
            {
                Text = "Текст подписи:",
                Location = new Point(10, 430)
            };
            Controls.Add(captionLabel);

            TextBox captionTextBox = new TextBox
            {
                Location = new Point(10, 460),
                Size = new Size(400, 60),
                Multiline = true
            };
            Controls.Add(captionTextBox);

            Button addCaptionButton = new Button
            {
                Text = "Добавить подпись",
                Location = new Point(10, 530),
                Size = new Size(120, 30)
            };
            addCaptionButton.Click += (sender, e) => AddCaptionToImage(pictureBox, captionTextBox.Text);
            Controls.Add(addCaptionButton);

        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            captionLocation = e.Location;
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                captionLocation = e.Location;
            }
        }
        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {

        }
        private void BrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Файлы изображений|*.jpg;*.jpeg;*.png;*.gif;*.bmp",
                Title = "Выберите изображение"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                pictureBox.Image = Image.FromFile(openFileDialog.FileName);
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }
        private void FontComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox fontComboBox = sender as ComboBox;
            selectedFontName = fontComboBox.SelectedItem.ToString();
        }

        private void FontSizeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown fontSizeNumericUpDown = sender as NumericUpDown;
            selectedFontSize = (int)fontSizeNumericUpDown.Value;
        }

        private void FontColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Button fontColorButton = sender as Button;
                fontColorButton.BackColor = colorDialog.Color;
                selectedFontColor = colorDialog.Color;
            }
        }
        private void AddCaptionToImage(PictureBox pictureBox, string caption)
        {
            if (pictureBox.Image == null)
            {
                MessageBox.Show("Выберите изображение перед добавлением подписи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Bitmap bitmap = new Bitmap(pictureBox.Image);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                if (captionFont != null)
                {
                    Brush brush = new SolidBrush(selectedFontColor);
                    StringFormat stringFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };

                    graphics.DrawString(caption, captionFont, brush, new RectangleF(captionLocation.X, captionLocation.Y, bitmap.Width, bitmap.Height), stringFormat);
                }

                pictureBox.Image = bitmap;

            }
        }
    }
}
    
