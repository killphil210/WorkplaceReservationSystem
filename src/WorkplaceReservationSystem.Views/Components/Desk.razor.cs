using Microsoft.AspNetCore.Components;
using WorkplaceReservationSystem.Domain;

namespace WorkplaceReservationSystem.Views.Components
{
    public partial class Desk
    {
        [Parameter] 
        public bool IsAvailable { get; set; } = true;
        
        [Parameter] 
        public bool IsSelected { get; set; }

        [Parameter] 
        public EventCallback<int> OnClick { get; set; }

        [Parameter] public Workplace Workplace { get; set; } = new Workplace();

        public string Color
        {
            get 
            {
                if (!IsAvailable)
                {
                    return "red";
                }
                else if (IsSelected)
                {
                    return "lightgrey";
                }
                else
                {
                    return "grey";
                }
            }
        }

        public void HandleClick()
        {
            if (IsAvailable)
            {
                OnClick.InvokeAsync(Workplace.Id);
            }
        }
    }
}