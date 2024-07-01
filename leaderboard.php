<?php
	$con = mysqli_connect('localhost', 'root', '', 'unitycreds', 3302);

	// check that connection happened
	if (mysqli_connect_errno()) {
		echo "1 : Connection Failed"; //error code #1 = connection failed
		exit();
	}

	$sql = "SELECT username, highscore FROM players ORDER BY highscore DESC";
	$result = mysqli_query($con, $sql);
	while($row = mysqli_fetch_assoc($result)) {
		echo $row['username'] . " " . $row['highscore'] . "\n";
	}
?>