<p align="center">
  <a href="" rel="noopener">
 <img width=200px height=200px src="https://i.imgur.com/6wj0hh6.jpg" alt="Project logo"></a>
</p>

<h3 align="center">Project Title</h3>

<div align="center">

</div>

---

<p align="center"> Few lines describing your project.
    <br> 
</p>

## 📝 Table of Contents

- [About](#about)
- [Getting Started](#getting_started)

## 🧐 About <a name = "about"></a>

Example Implementation of NLog with Fluent-bit

## 🏁 Getting Started <a name = "getting_started"></a>

```
docker-compose up -d --build
```

via the log of the console, can be seen the output

### Prerequisites

set the right address of the fluent-bit Container in the the nlog.config file:
- target->host

you get the network address by executing:
```
docker network inspect <containerID>
```
