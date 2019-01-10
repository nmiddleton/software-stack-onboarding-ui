<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"
				xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt"
                exclude-result-prefixes="msxsl">

	<xsl:template match="/">
		<table border="1px" cellpadding="0px" cellspacing="0px" width="800px" >
			<tr>
				<td align="center">
					<table border="0px" cellpadding="0px" cellspacing="10px">

						<tr>
							<td align="center" style="height:50px; background:Orange; color:White; font-size:large; " >GMI Management Profile</td>
						</tr>

						<tr>
							<td align="left" style="color:Gray; text-align:justify; font-size:smaller; " ><i>Note: The data presented below is a summary of your selections that have been set when the XML was created. The data used for import to GMI is held with in the XML tag Attributes. Manually changing any of the text data below does not change the management profile in any way.</i>
							</td>
						</tr>

						<!-- PROFILE : BEGIN -->
						<tr>
							<td align="left">
								<span style="font-weight:bold; text-decoration:underline;" >Profile Name: </span>
								<span style="font-weight:bold;" >
									<xsl:value-of select="PROFILE/PROFILE_CONTEXT/@GMI_MANAGEMENT_PROFILE"/>
								</span>
								<br />
								<span style="font-weight:bold; text-decoration:underline;" >Profile Version: </span>
								<span style="font-weight:bold;" >
									<xsl:value-of select="PROFILE/PROFILE_CONTEXT/@PROFILE_VERSION"/>
								</span>
							</td>
						</tr>
						<!-- PROFILE : END -->

						<!-- ITIC : BEGIN -->
						<tr>
							<td align="left">
								<table border="0px" cellpadding="0px" cellspacing="0px" >
									<tr>
										<td colspan="2" >
											<span style="font-weight:bold; text-decoration:underline;" >ITIC Information:</span>
										</td>
									</tr>
									<tr>
										<td>
											<span style="margin-left:15px;" >Infrastructure: </span>
											<span style="font-weight:bold;" ><xsl:value-of select="PROFILE/ITIC/@GMI_INFRASTRUCTURE"/></span>
										</td>
									</tr>
									<tr>
										<td>
											<span style="margin-left:15px;" >Capability: </span>
											<span style="font-weight:bold;" ><xsl:value-of select="PROFILE/ITIC/@GMI_CAPABILITY"/></span>
										</td>
									</tr>
									<tr>
										<td>
											<span style="margin-left:15px;" >Logical System Group: </span>
											<span style="font-weight:bold;" ><xsl:value-of select="PROFILE/ITIC/@GMI_SYSTEM_GROUP"/></span>
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<!-- ITIC : END -->


						<!-- LOCAL PROPERTIES : BEGIN -->
						<tr>
							<td align="left">
								<table border="0px" cellpadding="0px" cellspacing="0px" >
									<tr>
										<td>
											<span style="font-weight:bold; text-decoration:underline;" >Local Properties:</span>
										</td>
									</tr>
									<tr>
										<td>
											<table border="1px" cellpadding="3px" cellspacing="0px" style="margin-left:15px;" >
												<tr>
													<th>Name</th>
													<th>Key</th>
													<th>Value</th>
												</tr>
												<xsl:for-each select="PROFILE/LOCAL_PROPERTIES/LOCAL_PROPERTY">
													<tr>
														<td>
															<xsl:value-of select="@PROPERTY_NAME"/>
														</td>
														<td>
															<xsl:value-of select="@PROPERTY_KEY"/>
														</td>
														<td>
															<xsl:value-of select="@PROPERTY_VALUE"/>
														</td>
													</tr>
												</xsl:for-each>
											</table>
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<!-- LOCAL PROPERTIES : END -->

						<!-- ACTIONS : BEGIN -->
						<tr>
							<td align="left">
								<table border="0px" cellpadding="0px" cellspacing="0px" >
									<tr>
										<td>
											<span style="font-weight:bold; text-decoration:underline;" >Provisioning Plans:</span>
										</td>
									</tr>
									<tr>
										<td>
											<xsl:for-each select="PROFILE/PROV_PLANS/PROV_PLAN">
												<table border="0px" cellpadding="0px" cellspacing="0px" >
													<tr>
														<td>
															<span style="margin-left:15px;" >Provisioning Plan: </span>
															<span style="font-weight:bold;" ><xsl:value-of select="@PLAN_GROUP_TYPE"/></span>
														</td>
													</tr>

													<!-- ACTIONS : BEGIN -->
													<tr>
														<td>
															<span style="margin-left:15px;" >Actions:</span>
														</td>
													</tr>
													<tr>
														<td>
															<table border="1px" cellpadding="3px" cellspacing="0px" style="margin-left:30px;" >
																<tr>
																	<th>Order</th>
																	<th>Name</th>
																	<th>Continue on error</th>
																	<th>Max retries</th>
																</tr>
																<xsl:for-each select="PROV_PLAN_ACTIONS/ACTION">
																	<tr>
																		<td align="center" >
																			<xsl:value-of select="@RUNORDER"/>
																		</td>
																		<td>
																			<xsl:value-of select="@ACTION_TYPE_NAME"/>
																			<br/>
																			<xsl:for-each select="ACTION_PARAMETER">
																				- <xsl:value-of select="@VALUE"/><br/>
																			</xsl:for-each>
																		</td>
																		<td align="center" >
																			<xsl:choose>
																				<xsl:when test="@CONTINUE_ON_ERROR='Y'">
																					YES
																				</xsl:when>
																				<xsl:when test="@CONTINUE_ON_ERROR='N'">
																					NO
																				</xsl:when>
																				<xsl:otherwise>
																					<xsl:value-of select="@CONTINUE_ON_ERROR"/>
																				</xsl:otherwise>
																			</xsl:choose>
																		</td>
																		<td align="center" >
																			<xsl:value-of select="@MAX_RETRIES"/>
																		</td>
																	</tr>
																</xsl:for-each>
															</table>
														</td>
													</tr>
													<!-- ACTIONS : END -->

													<!-- PROPERTIES : BEGIN -->
													<tr>
														<td>
															<span style="margin-left:15px;" >Properties:</span>
														</td>
													</tr>
													<tr>
														<td>
															<table border="1px" cellpadding="3px" cellspacing="0px" style="margin-left:30px;" >
																<tr>
																	<th>Name</th>
																	<th>Value</th>
																</tr>
																<xsl:for-each select="PROV_PLAN_PROPERTIES/PLAN_PROPERTY">
																	<tr>
																		<td>
																			<xsl:value-of select="@PROPERTY_NAME"/>
																		</td>
																		<td>
																			<xsl:value-of select="@PROPERTY_VALUE"/>
																		</td>
																	</tr>
																</xsl:for-each>
															</table>
														</td>
													</tr>
													<!-- PROPERTIES : END -->

												</table>
											</xsl:for-each>
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<!-- ACTIONS : END -->

					</table>
				</td>
			</tr>
		</table>
	</xsl:template>
</xsl:stylesheet>
