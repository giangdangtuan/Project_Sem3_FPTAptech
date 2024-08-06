namespace Italy_Recipe_Page.Models.ViewModels
{
    public class CartItem
    {
        public int PackId { get; set; }
        public string PackName { get; set; }
        public int? Pack_Lenght { get; set; }

		public int SoLuong { get; set; }
		public int? ThanhTien => Pack_Lenght * SoLuong;
    }

}
