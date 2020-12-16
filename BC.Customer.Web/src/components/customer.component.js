import React, { Component } from "react";
import CustomerDataService from "../services/customer.service";
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

export default class Customer extends Component {
  constructor(props) {
    super(props);
    this.getCustomer = this.getCustomer.bind(this);
    
    this.state = {
        id: 0,
        description: "",
        firstName: "",
        lastName: "", 
        address: "",
        postalCode: "",
        phone: 0,
        imageName : "",
        imageUrl: "",
        file: null,
        filepreview: null,
        submitted: false
      
    };
  }

  componentDidMount() {
    this.getCustomer(this.props.match.params.id);
  }
 
  onChange =(e)=> {
    this.setState({
        
         ...this.state, [e.target.name]: e.target.value 
    });
  }

  async submit(e) {
  e.preventDefault();
      const formData = new FormData();
      formData.append('Id', this.state.id);
      formData.append('File', this.state.file);
      formData.append('FirstName', this.state.firstName);
      formData.append('LastName', this.state.lastName);
      formData.append('Address', this.state.address);
      formData.append('PostalCode', this.state.postalCode);
      formData.append('Phone', this.state.phone);
      formData.append('ImageName', this.state.imageName);
      formData.append('ImageUrl', this.state.imageUrl);
  
      CustomerDataService.create(formData)
        .then(response => {
      
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

  getCustomer(id) 
  {
     var data = {
        id: id
      
      };
    CustomerDataService.getAll(data)
      .then(response => {
      
        this.setState({
          id: response.data.returnObject[0].id,
          firstName: response.data.returnObject[0].firstName,
          lastName: response.data.returnObject[0].lastName,
          address: response.data.returnObject[0].address,
          postalCode: response.data.returnObject[0].postalCode,
          phone: response.data.returnObject[0].phone,
          imageName: response.data.returnObject[0].imageName,
          imageUrl :  response.data.returnObject[0].imageUrl,
        });
        console.log(response.data);
      })
      .catch(e => {
        console.log(e);
      });
  }
 
  setfile= (e) => {
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
    
 deleteCustomer =(e) => { 
   var data = {
    Id: this.state.id,
    ImageName: this.state.imageName
  };

  CustomerDataService.delete(data)
    .then(response => {
      console.log(response.data);
      this.props.history.push('/Customer')
    })
    .catch(e => {
      console.log(e);
    });
}

  render() {
   

    return (
     
          <div className="edit-form">
            <h4>Edit Customer Details</h4>
           
            <form onSubmit={e => this.submit(e)}>
            <div className="form-group">
        
           
            {this.state.filepreview ? (
            <img   alt="Profile" src={this.state.filepreview} className="Preview-Image"/>
            ):(
                <img   alt="Profile" src={this.state.imageUrl} className="Preview-Image"/>
            )}
             <input type="file"   accept="image/x-png,image/jpeg"  onChange={this.setfile} />
           
            </div>
            <div className="form-group">
              <label htmlFor="FirstName">First Name</label>
              <input
                type="text"
                className="form-control"
                required
                value={this.state.firstName}
                onChange={this.onChange}
                name="firstName"
              />
            </div>

            <div className="form-group">
              <label htmlFor="LastName">Last Name</label>
              <input
                type="text"
                className="form-control"
                id="lastName"
                required
                value={this.state.lastName}
                onChange={this.onChange}
                name="lastName"
              />
            </div>
            <div className="form-group">
              <label htmlFor="Address">Address</label>
              <input
                type="text"
                className="form-control"
                id="address"
                required
                value={this.state.address}
                onChange={this.onChange}
                name="address"
              />
            </div>
            <div className="form-group">
              <label htmlFor="PostalCode">Postal Code</label>
              <input
                type="text"
                className="form-control"
                id="postalCode"
                required
                value={this.state.postalCode}
                onChange={this.onChange}
                name="postalCode"
              />
            </div>
            <div className="form-group">
              <label htmlFor="Phone">Phone</label>
              <input
                type="number"
                className="form-control"
                id="phone"
                required
                value={this.state.phone}
                onChange={this.onChange}
                name="phone"
                maxLength = "9" 
                onInput={this.maxLengthCheck}
              />
            </div>
            <div class="row">
          <div class="col-sm-3" ><button  className="btn btn-success">
                  Submit 
                </button></div>
          <div class="col-sm-3" > 
          <button  className="btn btn-danger" onClick= {this.deleteCustomer} >Delete </button></div>
        </div>
              
          
           
            </form>
            
            <ToastContainer />
           
            
            </div>

    );
  }
}