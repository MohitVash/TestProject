stsadm -o addsolution -filename NCNewssiteApplicationPages.wsp
stsadm -o addsolution -filename NCNewssiteBranding.wsp
stsadm -o addsolution -filename NCNewssiteCommon.wsp
stsadm -o addsolution -filename NCNewssiteCore.wsp
stsadm -o addsolution -filename NCNewssiteLayout.wsp
stsadm -o addsolution -filename NCNewssitePatch1.wsp
stsadm -o addsolution -filename NCNewssitePatch2.wsp
stsadm -o addsolution -filename NCNewssitePageSetup.wsp

stsadm -o deploysolution -name "NCNewssiteApplicationPages.wsp" -immediate -allowGacDeployment -allowCasPolicies -force --allcontenturls
stsadm -o deploysolution -name "NCNewssiteBranding.wsp" -immediate -allowGacDeployment -allowCasPolicies -force
stsadm -o deploysolution -name "NCNewssiteCommon.wsp" -immediate -allowGacDeployment -allowCasPolicies -force --allcontenturls
stsadm -o deploysolution -name "NCNewssiteCore.wsp" -immediate -allowGacDeployment -allowCasPolicies -force
stsadm -o deploysolution -name "NCNewssiteLayout.wsp" -immediate -allowGacDeployment -allowCasPolicies -force
stsadm -o deploysolution -name "NCNewssitePatch1.wsp" -immediate -allowGacDeployment -allowCasPolicies -force --allcontenturls
stsadm -o deploysolution -name "NCNewssitePatch2.wsp" -immediate -allowGacDeployment -allowCasPolicies -force
stsadm -o deploysolution -name "NCNewssitePageSetup.wsp" -immediate -allowGacDeployment -allowCasPolicies -force -allcontenturls
