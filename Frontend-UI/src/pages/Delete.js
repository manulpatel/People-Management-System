import React, { useState, useEffect } from "react";
import { useNavigate, useLocation, useParams } from "react-router-dom";
import axios from "axios";
import { toHaveStyle } from "@testing-library/jest-dom/dist/matchers";
import "./AddEdit.css";
import { toast } from "react-toastify";

const Delete = async (id) => {
  const response = await axios.delete(
    "http://localhost:5018/peopleApi/people/{id}",
    id
  );
  if (response.status === 200) {
    toast.success(response.data);
  }
};
export default Delete;
