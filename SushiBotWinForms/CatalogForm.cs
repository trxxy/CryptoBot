using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Logic.DataTableObjects;
using Logic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace UserInterface
{
    public partial class CatalogForm : Form
    {
        private readonly ICatalogService _catalogService;
        private readonly UserSessionService _userSession;
        List<SushiDTO> SushiCatalog { get; set; }
        BasketDTO BasketDTO { get; set; }
        UserDTO CurrentUser { get; }

        int currentProductCount = 1;
        decimal currentTotalPrice = 0;

        public CatalogForm(IServiceProvider serviceProvider, UserSessionService userSession)
        {
            _catalogService = serviceProvider.GetRequiredService<ICatalogService>();
            _userSession = userSession;
            CurrentUser = userSession.CurrentUser;

            InitializeComponent();
            InitializeData();
        }

        protected override void OnClosed(EventArgs e)
        {
            _userSession.Dispose();
            Application.Exit();
        }

        private void btnProductMinus_Click(object sender, EventArgs e)
        {
            if (currentProductCount != 1) currentProductCount--;
            tbProductCount.Text = currentProductCount.ToString(); 
        }

        private void btnProductPlus_Click(object sender, EventArgs e)
        {
            currentProductCount++;
            tbProductCount.Text = currentProductCount.ToString();
        }

        private void btnAddToBasket_Click(object sender, EventArgs e)
        {
            var item = lbSushies.SelectedItem.ToString();

            currentTotalPrice += SushiCatalog.Find(q => q.Name == item).Price * currentProductCount;
            tbTotalPrice.Text = currentTotalPrice.ToString();
            BasketDTO.TotalPrice = currentTotalPrice;

            if (lbBasket.Items.Contains(lbSushies.SelectedItem.ToString()))
            {
                var key = BasketDTO.Sushies.Where(q => q.Key.Name == item).FirstOrDefault().Key;
                BasketDTO.Sushies[key] += currentProductCount;
            }
            else
            {
                lbBasket.Items.Add(item);
                BasketDTO.Sushies.Add(SushiCatalog.Find(q => q.Name == item), currentProductCount);
            }
        }

        private void lbBasket_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbSushies.ClearSelected();
            if (lbBasket.SelectedIndex != -1)
            {
                var selectedIndex = lbBasket.SelectedIndex;
                var selectedSushi = SushiCatalog[selectedIndex];
                FillSushiInfo(selectedSushi);
            }
        }

        private void lbSushies_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbBasket.ClearSelected();
            if (lbSushies.SelectedIndex != -1)
            {
                var selectedIndex = lbSushies.SelectedIndex;
                var selectedSushi = SushiCatalog[selectedIndex];
                FillSushiInfo(selectedSushi);
            }
        }

        private void btnMakeOrder_Click(object sender, EventArgs e)
        {
            _userSession.Basket = BasketDTO;
            _userSession.OpenOrderForm();
        }

        private void btnClearBasket_Click(object sender, EventArgs e)
        {
            BasketDTO = ClearBasket();
            currentTotalPrice = 0;
        }

        private void FillSushiInfo(SushiDTO selectedSushi)
        {
            pbSushiImage.LoadAsync(selectedSushi.ImageUrl);
            pbSushiImage.BackgroundImageLayout = ImageLayout.Center;

            tbSushiDescription.Text = selectedSushi.Description;
            tbSushiWeight.Text = selectedSushi.Weight.ToString();
            tbSushiPrice.Text = selectedSushi.Price.ToString();
            tbProductCount.Text = currentProductCount.ToString();
        }

        private void InitializeData()
        {
            SushiCatalog = _catalogService.GetSushiesCatalog();
            BasketDTO = ClearBasket();

            if (SushiCatalog.Count == 0)
            {
                _catalogService.ParseCatalog();
                SushiCatalog = _catalogService.GetSushiesCatalog(); // add exception
            }
            lbSushies.DataSource = SushiCatalog.Select(q => q.Name).ToList();

            tbProductCount.Text = currentProductCount.ToString();
            tbTotalPrice.Text = currentTotalPrice.ToString();

            var selectedIndex = lbSushies.SelectedIndex;
            var selectedSushi = SushiCatalog[selectedIndex];
            FillSushiInfo(selectedSushi);
        }

        private void UserExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _userSession.Dispose();
        }

        private BasketDTO ClearBasket()
        => new BasketDTO()
        {
            UserId = CurrentUser.Id,
            User = CurrentUser,
            Sushies = new Dictionary<SushiDTO, int>()
        };
    }
}