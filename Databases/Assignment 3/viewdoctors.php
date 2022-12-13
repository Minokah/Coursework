<html>

<head>
    <title>Hospital Database</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.2/dist/css/bootstrap.min.css" rel="stylesheet"
        integrity="sha384-Zenh87qX5JnK2Jl0vWa8Ck2rdkQ2Bzep5IDxbcnCeuOxjzrPF/et3URy9Bv1WTRi" crossorigin="anonymous">
    <link href="styles.css" rel="stylesheet">
</head>

<body>
    <?php
            include 'connectdb.php';
        ?>
    <div id="header" class="center">
        <br>
        <a href="index.html" id="homelink">
            <h1>Hospital Database</h1>
        </a>
        <p>Look and edit Doctors, Patients, and Hospital information.</p>
    </div>

    <div class="container">
        <div class="row mt-4 mb-2">
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="index.html">Home</a></li>
                    <li class="breadcrumb-item active" aria-current="page">View Doctors</li>
                </ol>
            </nav>
            <h2>View Doctors</h2>
        </div>
        <form action method="post">
            <div class="row">
                <div class="col-4">
                    <button type="button" class="btn btn-primary" onclick="this.form.submit()">Apply Filters</button>
                    <p>* options will reset but filters will be applied</p>
                </div>
                <div class="col-2">
                    <h5>Filter By</h5>
                    <select class="form-select" name="filterSpeciality" aria-label="Specialty">
                        <option value="None">None</option>
                        <?php
                                include 'php_doctor/getspecialities.php';
                            ?>
                    </select>
                </div>
                <div class="col-2">
                    <h5>Sort By</h5>
                    <div class="form-check">
                        <input class="form-check-input" type="radio" name="radioSort" value="lastname"
                            id="radioSortLastName" checked>
                        <label class="form-check-label" for="radioSortLastName">
                            Last Name
                        </label>
                    </div>
                    <div class="form-check">
                        <input class="form-check-input" type="radio" name="radioSort" value="birthdate"
                            id="radioSortBirthdate">
                        <label class="form-check-label" for="radioSortBirthdate">
                            Birthdate
                        </label>
                    </div>
                </div>
                <div class="col-2">
                    <h5>Order By</h5>
                    <div class="form-check">
                        <input class="form-check-input" type="radio" name="radioOrder" value="ASC" id="radioOrderAsc"
                            checked>
                        <label class="form-check-label" for="radioOrderAsc">
                            Ascending
                        </label>
                    </div>
                    <div class="form-check">
                        <input class="form-check-input" type="radio" name="radioOrder" value="DESC" id="radioOrderDesc">
                        <label class="form-check-label" for="radioOrderDesc">
                            Descending
                        </label>
                    </div>
                </div>
            </div>
        </form>
        <div class="row mt-4 mb-2">
            <h2>Results</h2>
        </div>
        <div class="row">
            <ul class="list-group">
                <table class="table">
                    <tr>
                        <th>License Number</th>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>License Date</th>
                        <th>Birth Date</th>
                        <th>Works At</th>
                        <th>Speciality</th>
                    </tr>
                    <?php
                            include 'php_doctor/getdoctors.php';
                        ?>
                </table>
            </ul>
        </div>
    </div>
    <br>
    <p class="center">😺 Made by a student whose student number ends in 51</p>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.2/dist/js/bootstrap.bundle.min.js"
        integrity="sha384-OERcA2EqjJCMA+/3y+gxIOqMEjwtxJY7qPCqsdltbNJuaOe923+mo//f6V8Qbsw3"
        crossorigin="anonymous"></script>
</body>

</html>