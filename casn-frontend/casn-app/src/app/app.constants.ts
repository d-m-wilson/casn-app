/* Define app-level constants which can be imported to any module */
export class Constants {
    public readonly APPT_TYPES = { 1: 'Lam to Complete',
                                   2: 'Lam Insert',
                                   3: 'Surgical',
                                   4: 'Ultrasound',
                                   5: 'Courthouse Appointment',
                                   6: 'Follow up Appointment' };

    public readonly MENU_ITEMS = [
      {name: "My Dashboard", address: "/", icon: "dashboard"},
      {name: "Schedule a Ride", address: "/caller", icon: "departure_board"},
      {name: "View Schedule", address: "/view-schedule", icon: "event_note"},
    ];
}
