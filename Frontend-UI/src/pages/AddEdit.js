import React, { useState, useEffect } from "react";
import { useNavigate, useLocation, useParams } from "react-router-dom";
import axios from "axios";
import "./AddEdit.css";
import { toast } from "react-toastify";

const initialState = {
  firstName: "",
  lastName: "",
  age: "",
};

const AddEdit = () => {
  const [state, setState] = useState(initialState);

  const history = useNavigate();
  const headers = {
    "Content-Type": "application/json",
  };
  const addPerson = async (data) => {
    const response = await axios.post(
      "http://localhost:5018/peopleApi/people",
      data,
      {
        headers: headers,
      }
    );
    if (response.status === 200) {
      toast.success(response.data);
    }
  };

  const updatePerson = async (data, id) => {
    const response = await axios.put(
      "http://localhost:5018/peopleApi/people",
      data
    );
    if (response.status === 200) {
      toast.success(response.data);
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!state.firstName) {
      toast.error("First Name can't be empty.");
      
    }
    if (state.firstName.length < 2) {
      toast.error("Minimum Length of First Name required is 2.");
      return;
    }
    if (state.lastName.length < 1) {
      toast.error("Last Name can't be empty.");
      return;
      
    }
    if (state.age < 18 || state.age > 60 || !state.age) {
      toast.error("Please enter age from 18 to 60.");
      return;
    } else {
      if (!id) {
        addPerson(state);
      } else {
        updatePerson(state, id);
      }
      window.location.href = "/";
    }
  };

  const { id } = useParams();

  useEffect(() => {
    if (id) {
      getPersonById(id);
    }
  }, [id]);

  const getPersonById = async (id) => {
    const response = await axios.get(
      `http://localhost:5018/peopleApi/people/` + id
    );
    if (response.status === 200) {
      setState(response.data.responseData);
    }
  };
  const handleInputChange = (e) => {
    let { name, value } = e.target;

    switch (name) {
      case "firstName":
        setState({ ...state, firstName: value });
        break;
      case "lastName":
        setState({ ...state, lastName: value });
        break;
      case "age":
        setState({ ...state, age: value });
        break;
    }
  };

  return (
    <div style={{ marginTop: "0px" }}>
      {id ? <h2>Update Person Details</h2> : <h2>Add New Person Details</h2>}
      <form
        style={{
          margin: "auto",
          padding: "15px",
          maxWidth: "400px",
          alignContent: "center",
        }}
        onSubmit={handleSubmit}
      >
        <label htmlFor="firstName">First Name</label>
        <input
          type="text"
          id="firstName"
          name="firstName"
          placeholder="Enter First Name..."
          onChange={handleInputChange}
          value={state.firstName}
        />
        <label htmlFor="lastName">Last Name</label>
        <input
          type="text"
          id="lastName"
          name="lastName"
          placeholder="Enter Last Name..."
          onChange={handleInputChange}
          value={state.lastName}
        />
        <label htmlFor="age">Age</label>
        <input
          type="number"
          id="age"
          name="age"
          placeholder="Enter Age..."
          onChange={handleInputChange}
          value={state.age}
        />
        <input type="submit" value={id ? "Update" : "Add"} />
      </form>
    </div>
  );
};

export default AddEdit;
