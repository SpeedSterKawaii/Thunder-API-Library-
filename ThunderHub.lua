-- Thunder Hub by SpeedSterKawaii --
-- You are not allowed to skid this --
warn("Thunder Hub - Script Executed")

if saveinstance then
    -- Do nothing
else
    hint("Please stop using some other exploit, use thunder to run thunderhub")
    -- PS: "hint" is only available on Thunder.
end)

local Effect  = Instance.new("BlurEffect") -- Blur Effect
Effect.Parent = game.Lighting

local ThunderLoading = Instance.new("ScreenGui") 
local Logo = Instance.new("ImageLabel")

ThunderLoading.Name = "ThunderLoading"
ThunderLoading.Parent = game.CoreGui 
ThunderLoading.ZIndexBehavior = Enum.ZIndexBehavior.Sibling

Logo.Name = "Logo" -
Logo.Parent = ThunderLoading
Logo.BackgroundColor3 = Color3.fromRGB(255, 255, 255)
Logo.BackgroundTransparency = 1.000
Logo.Position = UDim2.new(0.427343756, 0, 0.396386832, 0)
Logo.Size = UDim2.new(0, 186, 0, 194)
Logo.Image = "rbxassetid://6764952705"
Logo.ScaleType = Enum.ScaleType.Fit
wait(3)
for i = 1, 10 do
    Logo.ImageTransparency = Logo.ImageTransparency + 0.1
    Effect:Destroy()
    wait()
end
