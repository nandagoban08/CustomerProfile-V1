import React, { Component } from "react";
import CustomerDataService from "../services/customer.service";
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

export default class AddCustomer extends Component {
  constructor(props) {
    super(props);

    this.state = {
      Id: 0,
      FirstName: "",
      LastName: "", 
      Address: "",
      PostalCode: "",
      Phone: 0,
      ImageName : "",
      ImageUrl: "",
      file: null,
      filepreview: null,
      submitted: false
    };
  }

  onChange =(e)=> {
    this.setState({
        
         ...this.state, [e.target.name]: e.target.value 
    });
  }

  setfile=(e)=> {  
    this.setState({ 
      
      file: e.target.files[0],
      filepreview: URL.createObjectURL(e.target.files[0])
      
    });
    
  }
  maxLengthCheck = (object) => {
    if (object.target.value.length > object.target.maxLength) {
     object.target.value = object.target.value.slice(0, object.target.maxLength)
      }
    }
  


  async submit(e) {
    e.preventDefault();
      const formData = new FormData();
      for (var k in this.state) {
        if (this.state.hasOwnProperty(k)) {
          formData.append(k, this.state[k]);
        }
    }
      CustomerDataService.create(formData)
        .then(response => {
          this.setState({...response.data});
         
          if(response.data.isError === true)
          {
           
            for (var k in response.data.messages) {
                toast.error(response.data.messages[k].description)
          }
        }
        else
        {
          this.props.history.push("/Customer"); 
        }
          console.log(response.data);
        })
        .catch(e => {
          console.log(e);
        });
  
  }
  


  render() {
    return (
      <div className="submit-form">
            <form onSubmit={e => this.submit(e)}>
          <div>
            <div className="form-group">
            <h4>Add Customer Details</h4>
            <br></br>
            <h6>Profile Picture</h6>
            
            {this.state.file ? (
            <img src={this.state.filepreview} alt="Profile"  className="Preview-Image"/>
            ):(
              <p></p>
            )}
            <input type="file"  accept="image/x-png,image/jpeg"  onChange={this.setfile}  required/>
            </div>
            <div className="form-group">
              <label htmlFor="FirstName">First Name</label>
              <input
                type="text"
                className="form-control"
                id="FirstName"
                required
                value={this.state.FirstName}
                onChange={this.onChange}
                name="FirstName"
              />
            </div>

            <div className="form-group">
              <label htmlFor="LastName">Last Name</label>
              <input
                type="text"
                className="form-control"
                id="LastName"
                required
                value={this.state.LastName}
                onChange={this.onChange}
                name="LastName"
              />
            </div>
            <div className="form-group">
              <label htmlFor="Address">Address</label>
              <input
                type="text"
                className="form-control"
                id="Address"
                required
                value={this.state.Address}
                onChange={this.onChange}
                name="Address"
              />
            </div>
            <div className="form-group">
              <label htmlFor="PostalCode">Postal Code</label>
              <input
                type="text"
                className="form-control"
                id="PostalCode"
                required
                value={this.state.PostalCode}
                onChange={this.onChange}
                name="PostalCode"
               
              />
            </div>
            <div className="form-group">
              <label htmlFor="Phone">Phone</label>
              <input
                type="number"
                className="form-control"
                id="Phone"
                required
                value={this.state.Phone}
                onChange={this.onChange}
                name="Phone"
                 maxLength = "9" 
                onInput={this.maxLengthCheck}
              />
            </div>

            <button   className="btn btn-success">
              Submit
            </button>
          </div>
            </form>
                   
            <ToastContainer />
      </div>
    );
  }
}