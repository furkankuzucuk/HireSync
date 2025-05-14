import React from "react";
import { Card } from "react-bootstrap";

const CandidateHome = () => {
  const username = localStorage.getItem("username");

  return (
    <Card>
      <Card.Body>
        <Card.Title>📌 Hoş Geldiniz, {username}</Card.Title>
        <Card.Text>
          <strong>Bugün:</strong> {new Date().toLocaleDateString("tr-TR")}
        </Card.Text>
        <Card.Text>
          Buradan açık pozisyonlara göz atabilir veya başvurularınızı takip edebilirsiniz.
        </Card.Text>
      </Card.Body>
    </Card>
  );
};

export default CandidateHome;
