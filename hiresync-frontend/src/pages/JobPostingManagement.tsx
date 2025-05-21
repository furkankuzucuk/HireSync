import React, { useState, useEffect } from "react";
import axios from "axios";
import "../css/JobPostingManagement.css";

interface Department {
  departmentId: number;
  departmentName: string;
}

interface Job {
  jobId: number;
<<<<<<< Updated upstream
=======
  departmentId: number;
>>>>>>> Stashed changes
  jobName: string;
}

const JobPostingManagement = () => {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [departmentId, setDepartmentId] = useState<number>(0);
  const [departments, setDepartments] = useState<Department[]>([]);
  const [jobs, setJobs] = useState<Job[]>([]);
  const [jobId, setJobId] = useState<number>(0);
  const [successMessage, setSuccessMessage] = useState("");

  // Departmanları getir
  useEffect(() => {
    const fetchDepartments = async () => {
      try {
        const response = await axios.get("http://localhost:5065/api/departments");
        setDepartments(response.data);
        if (response.data.length > 0) {
<<<<<<< Updated upstream
          setDepartmentId(response.data[0].departmentId);
        }
      } catch (error: any) {
        console.error("Departmanlar alınamadı:", error.message);
=======
          const firstDeptId = response.data[0].departmentId;
          setDepartmentId(firstDeptId);
        }
      } catch (error: any) {
        console.error("Departmanlar yüklenemedi:", error.response?.data || error.message);
>>>>>>> Stashed changes
      }
    };

    fetchDepartments();
  }, []);

<<<<<<< Updated upstream
=======
  // Departman değişince iş pozisyonlarını getir
>>>>>>> Stashed changes
  useEffect(() => {
    const fetchJobs = async () => {
      try {
        if (departmentId === 0) return;
<<<<<<< Updated upstream
        const response = await axios.get(`http://localhost:5065/api/jobs/department/${departmentId}`);
        setJobs(response.data);
        if (response.data.length > 0) {
          setJobId(response.data[0].jobId);
=======

        const response = await axios.get(`http://localhost:5065/api/jobs/department/${departmentId}`);
        setJobs(response.data);

        if (response.data.length > 0) {
          setJobId(response.data[0].jobId); // varsayılan olarak ilk iş pozisyonunu seç
>>>>>>> Stashed changes
        } else {
          setJobId(0);
        }
      } catch (error: any) {
<<<<<<< Updated upstream
        console.error("İşler alınamadı:", error.message);
=======
        console.error("İşler yüklenemedi:", error.response?.data || error.message);
>>>>>>> Stashed changes
      }
    };

    fetchJobs();
  }, [departmentId]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      const newJobList = {
<<<<<<< Updated upstream
        departmentId,
        jobId,
        title,
        description,
=======
        departmentId: departmentId,
        jobId: jobId,
        description: description,
>>>>>>> Stashed changes
        createDate: new Date()
      };

      const response = await axios.post("http://localhost:5065/api/joblists", newJobList);
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
      if (response.status === 201) {
        setSuccessMessage("İş ilanı başarıyla oluşturuldu.");
        setTitle("");
        setDescription("");
        setJobId(0);
<<<<<<< Updated upstream
      }
    } catch (error: any) {
      console.error("İlan oluşturma hatası:", error.message);
=======

        // Departman resetlenmesin istersen bu kısmı çıkar
        if (departments.length > 0) {
          setDepartmentId(departments[0].departmentId);
        }
      }
    } catch (error: any) {
      console.error("İlan yayınlama hatası:", error.response?.data || error.message);
      setSuccessMessage("Bir hata oluştu: " + (error.response?.data || error.message));
>>>>>>> Stashed changes
    }
  };

  return (
    <div className="tab-content">
      <h2>İş İlanı Oluştur</h2>

      {successMessage && <div className="alert alert-success">{successMessage}</div>}

      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label>Departman:</label>
          <select className="form-control" value={departmentId} onChange={(e) => setDepartmentId(parseInt(e.target.value))} required>
            {departments.map((dept) => (
              <option key={dept.departmentId} value={dept.departmentId}>{dept.departmentName}</option>
            ))}
          </select>
        </div>

        <div className="mb-3">
          <label>İş Pozisyonu:</label>
<<<<<<< Updated upstream
          <select className="form-control" value={jobId} onChange={(e) => setJobId(parseInt(e.target.value))} required>
            {jobs.map((job) => (
              <option key={job.jobId} value={job.jobId}>{job.jobName}</option>
            ))}
          </select>
=======
          <select
            className="form-control"
            value={jobId}
            onChange={(e) => setJobId(parseInt(e.target.value))}
            required
            disabled={jobs.length === 0}
          >
            {jobs.length === 0 ? (
              <option>Bu departmanda iş pozisyonu yok</option>
            ) : (
              jobs.map((job) => (
                <option key={job.jobId} value={job.jobId}>
                  {job.jobName}
                </option>
              ))
            )}
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
>>>>>>> Stashed changes
        </div>

        <div className="mb-3">
          <label>İlan Başlığı:</label>
          <input className="form-control" value={title} onChange={(e) => setTitle(e.target.value)} required />
        </div>

        <div className="mb-3">
          <label>İlan Açıklaması:</label>
          <textarea className="form-control" value={description} onChange={(e) => setDescription(e.target.value)} required />
        </div>

        <button type="submit" className="btn btn-dark">Yayınla</button>
      </form>
    </div>
  );
};

export default JobPostingManagement;
