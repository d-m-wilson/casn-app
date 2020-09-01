/* Define app-level constants which can be imported to any module */
export class Constants {
    public readonly MENU_ITEMS = [
      {
        name: "Home",
        address: "/dashboard",
        icon: "home",
        dispatcherOnly: false
      },
      {
        name: "My Drives",
        address: "/my-drives",
        icon: "local_taxi",
        dispatcherOnly: false
      },
      {
        name: "My Achievements",
        address: "/stats",
        icon: "grade",
        dispatcherOnly: false
      },
      {
        name: "Schedule a Ride",
        address: "/caller",
        icon: "departure_board",
        dispatcherOnly: true
      },
      {
        name: "View Schedule",
        address: "/view-schedule",
        icon: "event_note",
        dispatcherOnly: false
      },
      // {
      //   name: "Message Volunteers",
      //   address: "/message",
      //   icon: "chat",
      //   dispatcherOnly: true
      // },
    ];

  // These routes are only accessible to dispatchers.
  public readonly RESTRICTED_ROUTES = [
    '/caller',
    '/message',
    '/appointment'
  ];

  public readonly SERVICE_PROVIDER_MAP_MARKERS = {
    1: 'assets/img/marker_clinic.png',
    2: 'assets/img/marker_court.png',
    3: 'assets/img/marker_lodging.png',
    4: 'assets/img/marker_airport.png',
    default: 'assets/img/marker_cluster.png'
  };

  public readonly WELCOME_MESSAGES = [
    "Volunteers do not necessarily have the time; they just have the heart. - Elizabeth Andrew",
    "Volunteering is the ultimate exercise in democracy.  You vote in elections once a year, but when you volunteer, you vote every day about the kind of community you want to live in.",
    "I am no longer accepting the things I cannot change. I am changing the things I cannot accept. - Angela Davis",
  ];
}
