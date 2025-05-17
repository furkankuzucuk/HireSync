import React, { useEffect, useState } from "react";
import axios from "axios";
import "../css/JobListings.css";

interface Department {
  departmentName: string;
}

interface Job {
  jobName: string;
}

interface JobList {
  jobListId: number;
  description: string;
  createDate: string;
  department: Department;
  job: Job;
}

const JobListings = () => {
  const [jobs, setJobs] = useState<JobList[]>([]);

  useEffect(() => {
    const fetchJobs = async () => {
      try {
        const token = localStorage.getItem("token");
        const response = await axios.get("/api/joblists", {
          headers: { Authorization: `Bearer ${token}` },
        });
        setJobs(response.data);
      } catch (error) {
        console.error("İş ilanları alınamadı:", error);
      }
    };

    fetchJobs();
  }, []);

  const handleApply = async (jobListId: number) => {
    try {
      const token = localStorage.getItem("token");
      const username = localStorage.getItem("username");

      if (!username) {
        alert("Kullanıcı adı bulunamadı.");
        return;
      }

      const resumePath = `/uploads/${username}_cv.pdf`; // veya .docx uzantısı olabilir

      const application = {
        jobListId,
        appMail: `${username}@example.com`,
        location: "Bilinmiyor",
        appDate: new Date().toISOString(),
        resumePath,
        status: "Başvuru Yapıldı"
      };

      await axios.post("/api/jobapplications", application, {
        headers: { Authorization: `Bearer ${token}` },
      });

      alert("Başvuru başarıyla yapıldı.");
    } catch (error) {
      console.error("Başvuru yapılamadı:", error);
      alert("Başvuru sırasında hata oluştu.");
    }
  };

  return (
    <div className="container">
      <h2 className="mb-4">Açık Pozisyonlar</h2>

      {jobs.map((job) => (
        <div key={job.jobListId} className="card mb-3">
          <div className="card-body">
            <h5 className="card-title">{job.job?.jobName ?? "İş Adı Yok"}</h5>
            <p className="card-text">{job.description}</p>
            <p className="card-text">
              <strong>Departman:</strong> {job.department?.departmentName ?? "Departman Yok"}
            </p>
            <p className="card-text">
              <strong>Yayın Tarihi:</strong>{" "}
              {new Date(job.createDate).toLocaleDateString()}
            </p>
            <button
              className="btn btn-success"
              onClick={() => handleApply(job.jobListId)}
            >
              Başvur
            </button>
          </div>
        </div>
      ))}
    </div>
  );
};

export default JobListings;
