# CASN App

## Front End - Developing Locally
### Prerequisites
- Node.js (the app was developed using v10.1.0)
- npm (https://www.npmjs.com)

### Instructions
The Angular frontend is located in the /casn-frontend directory. To run locally:
- Clone the Github repository to your local machine
```bash
git clone https://github.com/d-m-wilson/casn-app.git
```
- From your terminal, cd into the frontend directory:
```bash
cd casn-app/casn-frontend/
```
- Using npm, ensure you have the Angular CLI installed, then install the app dependencies defined in **package.json**:
```bash
npm install -g @angular/cli
npm install
```
- After installation, you may build the app and run the Angular development server locally:
```bash
ng serve
```
- To build the app for production, run
```bash
ng build --configuration=prod
```
The static files you need to serve will be generated in the **/dist** folder.
