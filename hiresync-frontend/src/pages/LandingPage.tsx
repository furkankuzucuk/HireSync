import React, { useState, useEffect } from "react";
import axios from "axios";
import { Link } from "react-router-dom";
import "../css/LandingPage.css";
import logo from "../image/hiresync.jpeg";

const LandingPage = () => {
  const [jobListings, setJobListings] = useState<any[]>([]);

  useEffect(() => {
    const fetchJobListings = async () => {
      try {
        const response = await axios.get("http://localhost:5065/api/joblists");
        setJobListings(response.data);
      } catch (error) {
        console.error("İş ilanları alınamadı:", error);
      }
    };
    fetchJobListings();
  }, []);

  return (
    <div className="container-fluid p-0 lp-container">
      <header className="bg-white border-bottom py-3">
        <div className="container d-flex align-items-center justify-content-between">
          <img src={logo} alt="HireSync Logo" style={{ height: "100px" }} />
          <Link to="/login" className="btn btn-outline-primary">Giriş Yap</Link>
        </div>
      </header>

      <section className="hero bg-primary text-white text-center py-5">
        <div className="container">
          <h1 className="display-5 fw-bold">HireSync - İnsan Kaynakları Platformu</h1>
        </div>
      </section>

      <section className="job-listings py-5 bg-light">
        <div className="container">
          <h2 className="text-center mb-4">İş İlanları</h2>
          <div className="row">
            {jobListings.length > 0 ? (
              jobListings.map((job) => (
                <div className="col-md-4 mb-4" key={job.jobListId}>
                  <div className="card h-100 shadow-sm border-0 text-center">
                    <div className="card-body d-flex flex-column justify-content-between">
                      <h5 className="card-title fw-bold">{job.title}</h5>
                      <Link to={`/job-details/${job.jobListId}`} className="btn btn-sm btn-primary mt-3">
                        Detayları Gör
                      </Link>
                    </div>
                  </div>
                </div>
              ))
            ) : (
              <p className="text-center">Henüz iş ilanı yok.</p>
            )}
          </div>
        </div>
      </section>
    </div>
  );
};

export default LandingPage;
