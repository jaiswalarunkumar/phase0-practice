-- Phase 0 SQL practice (PostgreSQL)
CREATE TABLE clients (
    client_id SERIAL PRIMARY KEY,
    client_name VARCHAR(100) NOT NULL,
    city VARCHAR(50)
);

CREATE TABLE invoices (
    invoice_id SERIAL PRIMARY KEY,
    client_id INT NOT NULL REFERENCES clients(client_id),
    amount NUMERIC(12,2) NOT NULL CHECK (amount > 0),
    status VARCHAR(20) NOT NULL DEFAULT 'Unpaid',
    invoice_date DATE NOT NULL
);

INSERT INTO clients (client_name, city) VALUES
('Ravi Traders', 'Delhi'),
('Sita Stores', 'Mumbai'),
('Mohan & Co', 'Delhi'),
('Anil Hardware', 'Pune');

INSERT INTO invoices (client_id, amount, status, invoice_date) VALUES
(1, 5000, 'Paid',   '2026-10-01'),
(1, 3000, 'Unpaid', '2026-10-15'),
(2, 12000, 'Unpaid', '2026-10-20'),
(2, 2000, 'Paid',   '2026-11-02'),
(3, 8000, 'Paid',   '2026-11-05');

-- A: INNER JOIN
SELECT c.client_name, i.invoice_id, i.amount, i.status
FROM clients c
INNER JOIN invoices i ON c.client_id = i.client_id;

-- B: LEFT JOIN + GROUP BY (clients with 0 invoices also shown)
SELECT c.client_name,
       COUNT(i.invoice_id) AS total_invoices,
       COALESCE(SUM(i.amount), 0) AS total_amount
FROM clients c
LEFT JOIN invoices i ON c.client_id = i.client_id
GROUP BY c.client_id, c.client_name
ORDER BY total_amount DESC, c.client_name;

-- C: WHERE vs HAVING
SELECT c.client_name, SUM(i.amount) AS unpaid_total
FROM clients c
JOIN invoices i ON c.client_id = i.client_id
WHERE i.status = 'Unpaid'
GROUP BY c.client_name
HAVING SUM(i.amount) > 5000;
