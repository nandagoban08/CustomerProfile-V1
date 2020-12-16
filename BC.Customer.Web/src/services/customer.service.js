import http from "../http-common";

class CustomerDataService {


  create(data){
    return http.post("/Customer/SaveCustomer",data);
  }
  getAll(data) {
    return http.post("/Customer/getcustomers",data);
  }

  delete(data) {
    return http.post("/Customer/deletecustomer",data);
  }



}

export default new CustomerDataService();