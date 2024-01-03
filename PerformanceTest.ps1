param(
    [string]$Url,         # URL of the endpoint to test
    [int]$RequestCount    # Number of times to call the endpoint
)

# Initialize a hashtable to keep count of HTTP status codes
$httpStatusCounts = @{}

# Function to measure the duration of a web request
function Measure-WebRequest {
    param([string]$Url)

    try {
        $startTime = Get-Date
        $response = Invoke-WebRequest -Uri $Url -ErrorAction SilentlyContinue
        $endTime = Get-Date

        # Count the HTTP status code
        $statusCode = $response.StatusCode
        if ($httpStatusCounts[$statusCode] -eq $null) {
            $httpStatusCounts[$statusCode] = 1
        }
        else {
            $httpStatusCounts[$statusCode]++
        }

        return ($endTime - $startTime).TotalMilliSeconds
    }
    catch {
        # In case of a network error, or server returns an error code (4xx, 5xx)
        if ($httpStatusCounts["Error"] -eq $null) {
            $httpStatusCounts["Error"] = 1
        }
        else {
            $httpStatusCounts["Error"]++
        }
        return $null
    }
}

# Main Script Logic
$totalTime = 0
for ($i=0; $i -lt $RequestCount; $i++) {
    $progress = ($i / $RequestCount) * 100
    $status = "Processing request $($i + 1) of $RequestCount"
    Write-Progress -Activity "Performing Web Requests" -Status $status -PercentComplete $progress

    $duration = Measure-WebRequest -Url $Url
    if ($duration -ne $null) {
        $totalTime += $duration
    }
}

if ($RequestCount -gt 0) {
    $averageTime = $totalTime / $RequestCount
} else {
    $averageTime = 0
}

Write-Progress -Activity "Performing Web Requests" -Completed
Write-Host "Average Request Duration: $averageTime milliseconds"

# Report the HTTP status codes received
Write-Host "HTTP Status Counts;"
foreach ($code in $httpStatusCounts.Keys) {
    Write-Host "${code}: $($httpStatusCounts[$code])"
}
