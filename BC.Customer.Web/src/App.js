import React, { Component } from "react";
import { Switch, Route, Link } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import './App.css';

import AddCustomer from "./components/add-customer-component";
 import Customer from "./components/customer.component";
 import CustomerList from "./components/customer-list-component";

function App() {
  return (
    <div>
        <nav className="navbar navbar-expand navbar-dark bg-dark">
          <a href="/Customer" className="navbar-brand">
          Book cabin
          </a>
          <div className="navbar-nav mr-auto">
            <li className="nav-item">
              <Link to={"/Customer"} className="nav-link">
                Customers
              </Link>
            </li>
            <li className="nav-item">
              <Link to={"/add"} className="nav-link">
                Add
              </Link>
            </li>
          </div>
        </nav>

        <div className="container mt-3">
          <Switch>
            { <Route exact path={["/", "/Customer"]} component={CustomerList} /> }
            <Route exact path="/add" component={AddCustomer} />
            { <Route path="/Customer/:id" component={Customer} /> }
          </Switch>
        </div>
      </div>
  );
}

export default App;
