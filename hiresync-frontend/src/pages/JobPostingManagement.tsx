import React, { useState, useEffect } from "react";
import axios from "axios";
import "../css/JobPostingManagement.css";

interface Department {
  departmentId: number;
  departmentName: string;
}

const JobPostingManagement = () => {
  const [description, setDescription] = useState("");
  const [departmentId, setDepartmentId] = useState<number>(0);
  const [departments, setDepartments] = useState<Department[]>([]);
  const [successMessage, setSuccessMessage] = useState("");

  useEffect(() => {
    const fetchDepartments = async () => {
      try {
        const response = await axios.get("http://localhost:5065/api/departments");
        setDepartments(response.data);

        if (response.data.length > 0) {
          setDepartmentId(response.data[0].departmentId); // ilk departmanı seç
        }
      } catch (error: any) {
        console.error("Departmanlar yüklenemedi:", error.response ? error.response.data : error.message);
      }
    };

    fetchDepartments();
  }, []);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      const newJob = {
        departmentId: departmentId,
        description: description,
      };

      const response = await axios.post("http://localhost:5065/api/joblists", newJob);

      if (response.status === 201) {
        setSuccessMessage("İş ilanı başarıyla yayınlandı!");
        setDescription("");
        if (departments.length > 0) {
          setDepartmentId(departments[0].departmentId);
        }
      }
    } catch (error: any) {
      console.error("İlan yayınlama hatası:", error.response ? error.response.data : error.message);
      setSuccessMessage("Bir hata oluştu: " + (error.response?.data || error.message));
    }
  };

  return (
    <div className="tab-content">
      <h2>📌 İş İlanları Yönetimi</h2>

      {successMessage && (
        <div className="alert alert-success" role="alert">
          {successMessage}
        </div>
      )}

      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label>Departman:</label>
          <select
            className="form-control"
            value={departmentId}
            onChange={(e) => setDepartmentId(parseInt(e.target.value))}
            required
          >
            {departments.map((dept) => (
              <option key={dept.departmentId} value={dept.departmentId}>
                {dept.departmentName}
              </option>
            ))}
          </select>
        </div>

        <div className="mb-3">
          <label>İlan Açıklaması:</label>
          <textarea
            className="form-control"
            placeholder="İlan açıklaması"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            required
          />
        </div>

        <button type="submit" className="btn btn-dark">
          İlanı Yayınla
        </button>
      </form>
    </div>
  );
};

export default JobPostingManagement;
