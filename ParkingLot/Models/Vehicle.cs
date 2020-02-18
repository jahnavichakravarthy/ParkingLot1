
namespace ParkingLot.Models
{
    public class Vehicle
    {
        public VehicleModel Type { get; set; }
        public string VehicleNumber { get; set; }
    }
    public class TwoWheeler : Vehicle
    {
        TwoWheeler()
        {
            this.Type = VehicleModel.TwoWheeler;
        }
      
    }
    public class FourWheeler : Vehicle
    {
        FourWheeler()
        {
            this.Type = VehicleModel.FourWheeler;
        }
    }
    public class HeavyVehicle : Vehicle
    {
        HeavyVehicle()
        {
            this.Type = VehicleModel.HeavyVehicle;
        }
    }
}
