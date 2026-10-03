using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EShoppingLab4.CodeOnly
{
    public partial class FrmProducts : Form
    {
        private AppServices _services;

        private List<Product> _products =
            new List<Product>();

        public FrmProducts()
        {
            InitializeComponent();
        }

        public FrmProducts(AppServices services) : this()
        {
            _services = services;
            lblMode.Text = services.ModeText;
            LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                _products =
                    _services.Products.GetAll().ToList();

                cboGroup.Items.Clear();
                cboGroup.Items.Add("Tất cả");

                foreach (string group in
                         _products.Select(x => x.GroupName)
                                  .Distinct()
                                  .OrderBy(x => x))
                {
                    cboGroup.Items.Add(group);
                }

                cboGroup.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không tải được sản phẩm.\n" + ex.Message,
                    "Lỗi"
                );
            }
        }

        private void cboGroup_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (_services == null) return;

            string group = cboGroup.SelectedIndex <= 0
                ? null
                : cboGroup.SelectedItem.ToString();

            dgvProducts.DataSource = group == null
                ? _products.ToList()
                : _products
                    .Where(x => x.GroupName == group)
                    .ToList();
        }

        private Product SelectedProduct()
        {
            if (dgvProducts.CurrentRow == null)
                return null;

            return dgvProducts.CurrentRow.DataBoundItem
                as Product;
        }

        private void btnDetail_Click(
            object sender,
            EventArgs e)
        {
            Product p = SelectedProduct();

            if (p == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn sản phẩm.");
                return;
            }

            string text =
                "Mã sản phẩm: " + p.ProductCode +
                "\n\nTên sản phẩm: " + p.ProductName +
                "\n\nNhà sản xuất: " + p.Manufacturer +
                "\n\nNhóm: " + p.GroupName +
                "\n\nGiá: " + p.Price.ToString("N0") + " đ" +
                "\n\nTồn kho: " + p.StockQuantity +
                "\n\nTình trạng: " + p.StockStatus +
                "\n\nMô tả: " + p.Description +
                "\n\nThông số: " + p.Specifications;

            MessageBox.Show(
                text,
                "Chi tiết sản phẩm",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void btnAddCart_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                _services.Cart.Add(SelectedProduct());

                MessageBox.Show(
                    "Đã thêm vào giỏ hàng.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }
    }
}