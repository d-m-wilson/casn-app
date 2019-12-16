export const environment = {
  production: true,
  apiUrl: 'https://testapi.casn.app/api',
  clientRoot: 'https://test.casn.app/',
  customConfigUrl: 'https://test.casn.app/customconfig.json',
  get logoutRedirectUrl() { return `${this.clientRoot}assets/oidc-login-redirect.html`; },
};
