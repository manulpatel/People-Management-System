import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import "./Home.css";
import axios from "axios";
import { toast } from "react-toastify";

const Home = () => {
  const [data, setData] = useState([]);

  useEffect(() => {
    getUsers();
  }, []);

  const getUsers = async () => {
    const response = await axios.get("http://localhost:5018/peopleApi/people");
    if (response.status === 200) {
      setData(response.data);
    }
  };

  const onDeletePerson = async (id) => {
    if (
      window.confirm("Are you sure that you want to delete the person record?")
    ) {
      const response = await axios.delete(
        `http://localhost:5018/peopleApi/people/${id}`
      );
      if (response.status === 200) {
        toast.success(response.data);
        getUsers();
      }
    }
  };

  console.log("data=>", data);
  return (
    <div style={{ marginTop: "0" }}>
      <h2>People Records</h2>
      <table className="styled-table">
        <thead>
          <tr>
            <th style={{ textAlign: "center" }}>Sr. No.</th>
            <th style={{ textAlign: "center" }}>First Name</th>
            <th style={{ textAlign: "center" }}>Last Name</th>
            <th style={{ textAlign: "center" }}>Age</th>
            <th style={{ textAlign: "center" }}>Action</th>
          </tr>
        </thead>
        <tbody>
          {data &&
            data.responseData &&
            data.responseData.map((item, index) => {
              return (
                <tr key={index}>
                  <th scope="row">{index + 1}</th>
                  <td>{item.firstName}</td>
                  <td>{item.lastName}</td>
                  <td>{item.age}</td>
                  <td>
                    <Link to={`/update/${item.id}`} className="btn btn-edit">
                      Edit
                    </Link>
                    <button
                      onClick={() => onDeletePerson(item.id)}
                      className="btn btn-delete"
                    >
                      Delete
                    </button>
                  </td>
                </tr>
              );
            })}
        </tbody>
      </table>
    </div>
  );
};

export default Home;
