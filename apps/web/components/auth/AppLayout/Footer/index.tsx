import {
  FooterWrapper,
  GreenWrapper,
  LogoWrapper,
  CopyrightWrapper,
  SocialMediaWrapper,
  FacebookIcon,
  InstagramIcon,
  TwitterIcon,
  YoutubeIcon,
} from "./styles"

const Footer = () => (
  <FooterWrapper>
    <LogoWrapper>
      Tatuaz<GreenWrapper>App</GreenWrapper>
    </LogoWrapper>
    <CopyrightWrapper>Copyright 2023</CopyrightWrapper>
    <SocialMediaWrapper>
      <TwitterIcon />
      <YoutubeIcon />
      <InstagramIcon />
      <FacebookIcon />
    </SocialMediaWrapper>
  </FooterWrapper>
)

export default Footer
