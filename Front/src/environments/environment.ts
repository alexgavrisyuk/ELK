// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export class Environment {
  static IsProduction = true;
}

export class Local {
  static AUTH_SERVICE_URL = 'http://localhost:5000/api';
  static ORDERS_SERVICE_URL = 'http://localhost:5002/api';
  static CUSTOMER_SERVICE_URL = 'http://localhost:5001/api';
}

export class Production {
  static AUTH_SERVICE_URL = 'http://192.168.99.100:5000/api';
  static ORDERS_SERVICE_URL = 'http://192.168.99.100:5002/api';
  static CUSTOMER_SERVICE_URL = 'http://192.168.99.100:5001/api';
}

/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
