import React, { Component } from "react";
import CustomerDataService from "../services/customer.service";

export default class customerList extends Component {
  constructor(props) {
    super(props);
   
    this.state = {
      customers: [],
      searchByNames: "",
      firstname: "",
      lastname: "",
      Address: "",
      PostalCode: "",
      Phone: 0,
      ImageName : "",
      ImageUrl: "",
      
    };
  }

  componentDidMount() {
    this.retrieveCustomers();
  }

  onChangeFirstName=(e)=> {
    this.setState({
        firstname: e.target.value
        
    });
  }

  onChangeLastName=(e)=> {
    this.setState({
        lastname: e.target.value
    });
  }

  retrieveCustomers=(e)=> {
    var data = {
        firstname: this.state.firstname,
        lastname: this.state.lastname
      };

    CustomerDataService.getAll(data)
      .then(response => {
        this.setState({ 
          customers: response.data.returnObject
        });
        console.log(response.data);
      })
      .catch(e => {
        console.log(e);
      });
  }


  searchByNames=(e)=> {
    var data = {
        firstname: this.state.firstname,
        lastname: this.state.lastname
      };
    CustomerDataService.getAll(data)
      .then(response => {
        this.setState({
          customers:Array.from(response.data.returnObject)
        });
        console.log(response.data);
      })
      .catch(e => {
        console.log(e);
      });
  }

  renderTableHeader() {
       return   <tr>  
        <th>First Name</th>
        <th>Last Name</th>
        <th>Address</th>
        <th>Postal Code</th>
        <th>Phone</th>
        <th></th>
        <th></th>
        </tr>
 }

  renderTableData() {
    return this.state.customers.map((customer, index)  => {
       const { id, firstName, lastName, address,imageUrl,postalCode,phone } = customer //destructuring
       return (    
          <tr key={id} >  
             <td>{firstName}</td>
             <td>{lastName}</td>
             <td>{address}</td>
             <td>{postalCode}</td>
             <td>{phone}</td>
             <td> <img  alt="Profile" src={imageUrl}  className="Preview-Image-smaller"/></td>
             <td><button  className="badge badge-warning" onClick={() => this.props.history.push("/customer/" + customer.id)}>Edit </button></td>
             
          </tr>      
       )
    })
 }


  render() {
   

    return (
        <div className="list row">
        <div className="col-md-12">
          <div className="input-group mb-3">
            <input
              type="text"
              className="form-control"
              placeholder="Search by First Name"
           
              onChange={this.onChangeFirstName}
            />
         
            <input
              type="text"
              className="form-control"
              placeholder="Search by Last Name"
              onChange={this.onChangeLastName}
            />
            <div className="input-group-append">
              <button
                className="btn btn-outline-secondary"
                type="button"
                onClick={this.searchByNames}
              >
                Search
              </button>
            </div>
          </div>
        </div>
        <div className="col-md-6">
          <h4>Customer List</h4>
          <table id='customer'>
               <tbody>
               {this.renderTableHeader()}
                  {this.renderTableData()}
               </tbody>
            </table>
        </div>
       
      </div>
          

    );
  }
}