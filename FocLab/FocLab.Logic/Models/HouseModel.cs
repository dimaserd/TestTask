namespace FocLab.Logic.Models
{
    public class HouseModel
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public WaterCounterModel WaterCounter { get; set; }
    }
}
