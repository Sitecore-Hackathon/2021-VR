# escape=`

ARG BASE_IMAGE
ARG SOLUTION_IMAGE
ARG TOOLING_IMAGE
ARG JSS_RESOURCES_IMAGE

FROM ${SOLUTION_IMAGE} as solution
FROM ${TOOLING_IMAGE} as tooling
FROM ${JSS_RESOURCES_IMAGE} as jss_resources
FROM ${BASE_IMAGE}

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

WORKDIR C:\inetpub\wwwroot

# Add developer tools and entrypoint
COPY --from=tooling C:\tools C:\tools

# Copy and init the JSS / Headless Services Module
COPY --from=jss_resources C:\module\cd\content C:\inetpub\wwwroot
COPY --from=jss_resources C:\module\tools C:\module\tools
RUN C:\module\tools\Initialize-Content.ps1 -TargetPath C:\inetpub\wwwroot; `
    Remove-Item -Path C:\module -Recurse -Force;

# Add solution website files
COPY --from=solution /artifacts/sitecore/ ./