export const environment = {
  production: true,
  apiUrl: 'https://casnapptest.dmwilson.info/api',
  clientRoot: 'https://casnapptest.dmwilson.info/',
  customConfigUrl: 'https://casnapptest.dmwilson.info/customconfig.json',
  get logoutRedirectUrl() { return `${this.clientRoot}assets/oidc-login-redirect.html`; },
};
