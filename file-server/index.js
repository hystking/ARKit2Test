const express = require('express');
const app = express();
const fs = require('fs');
const crypto = require('crypto');
const bodyParser = require('body-parser');

app.use(express.static(`${__dirname}/public`));
app.listen(3000);
app.use(
  bodyParser.raw({
    limit: '10mb',
  }),
);

app.post('/upload', (req, res) => {
  console.log('upload');
  console.log(req.body);
  const hash = crypto
    .createHmac('sha256', '')
    .update(req.body)
    .digest('hex');
  fs.writeFileSync(`${__dirname}/public/files/${hash}`, req.body);
  res.send(`/files/${hash}`);
});
