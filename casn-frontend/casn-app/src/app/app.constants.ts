/* Define app-level constants which can be imported to any module */
export class Constants {
    public readonly MENU_ITEMS = [
      {name: "Home", address: "/", icon: "dashboard"},
      {name: "My Achievements", address: "/stats", icon: "grade"},
      {name: "Schedule a Ride", address: "/caller", icon: "departure_board"},
      {name: "View Schedule", address: "/view-schedule", icon: "event_note"},
    ];
}
