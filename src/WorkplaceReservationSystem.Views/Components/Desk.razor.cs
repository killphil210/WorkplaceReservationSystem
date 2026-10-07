using Microsoft.AspNetCore.Components;
using WorkplaceReservationSystem.Domain;

namespace WorkplaceReservationSystem.Views.Components
{
    public partial class Desk
    {
        [Parameter] public double X { get; set; }
        [Parameter] public double Y { get; set; }

        [Parameter] public Workplace Workplace { get; set; } = new Workplace();

        public void OnClick()
        {
            if (Workplace.IsReserved == false)
            {
                Workplace.Reservation = new Reservation();
            }
            else
            {
                Workplace.Reservation = null;
            }
        }

        public string GetColor()
        {
            if (Workplace.IsReserved)
            {
                return "lightgrey";
            }
            else
            {
                return "grey";
            }
        }
    }
}